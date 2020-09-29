using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;

namespace FormationOfRevaluation
{
    public partial class frmCreateReval : Form
    {
        int idTreq, id_dep;        
        DataTable dt;
        DateTime data_outt;
        int ntypeorg;
        int idAll = 0;

        /// <summary>
        /// Процедура инициализации формы
        /// </summary>
        /// <param name="_idTreq">id заявки</param>
        /// <param name="_dt">таблица со списком товаров по заявке</param>
        /// <param name="_data_outt">дата акта переоценки</param>
        /// <param name="_ntypeorg">тип юл</param>
        /// <param name="_id_dep">id отдела</param>
        public frmCreateReval(int _idTreq, DataTable _dt, DateTime _data_outt, int _ntypeorg, int _id_dep)
        {
            idTreq = _idTreq;            
            dt = _dt;
            data_outt = _data_outt;
            ntypeorg = _ntypeorg;
            id_dep = _id_dep;
            InitializeComponent();
        }

        private void frmCreateReval_Load(object sender, EventArgs e)
        {
            Config.CurDate = Config.hCntMain.getdate();
            dtpDate.MinDate = Config.CurDate.Date;
            
            if (Config.CurDate.Date < data_outt.Date)
            {
                dtpDate.Value = data_outt.Date;
            }
            else
            {
                dtpDate.Value = Config.CurDate.Date;
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            dt.Columns.Add("old_rcena", typeof(decimal));
            dt.AcceptChanges();

            DataTable dtDetails;

            Logging.StartFirstLevel(81);
            Logging.Comment("Начало добавления переоценки по заявке № " + idTreq.ToString());
            

            for (int i = 0; dt.Rows.Count > i; i++)
            {
                int id_tov  = int.Parse(dt.Rows[i]["id_tovar"].ToString());
                decimal zcenabnds = decimal.Parse(dt.Rows[i]["zcenabnds"].ToString());
                    
                dtDetails = new DataTable();
                dtDetails = Config.hCntMain.AddEditRcena(dtpDate.Value.Date, id_tov, zcenabnds);

                dt.Rows[i]["old_rcena"] = decimal.Parse(dtDetails.Rows[0]["old_rcena"].ToString());

                Logging.Comment("id товара: " + dt.Rows[i]["id_tovar"].ToString()
                    + ", EAN: " + dt.Rows[i]["ean"].ToString()
                    + ", Наименование: " + dt.Rows[i]["cname"].ToString()
                    + ", Цена продажи: " + dt.Rows[i]["zcenabnds"].ToString()                    
                    );
            }

            Config.hCntMain.UpdateTreq(idTreq);

            Logging.Comment("Окончание добавления переоценки по заявке № " + idTreq.ToString());
            Logging.StopFirstLevel();

            DataTable dtCopy = dt.Copy();

            //удаляем строки которые были добавлены
            int y = dtCopy.Rows.Count;
            while (y != 0)
            {
                if (decimal.Parse(dtCopy.Rows[y - 1]["old_rcena"].ToString()) == 0)
                {
                    dtCopy.Rows.RemoveAt(y - 1);
                }
                y--;
            }

            if (dtCopy.Rows.Count > 0)
            {
                //открываем форму с сообщением
                frmUpdatedGoods frmUpGoods = new frmUpdatedGoods(dtCopy);
                frmUpGoods.ShowDialog();                
            }

            if (Config.hCntMain.getSettings(118, "crr") != 0 && ntypeorg != 0)
                CreateActPereoc();
            else if (Config.hCntMain.getSettings(118, "crr") != 0 && ntypeorg == 0)
                MessageBox.Show("Ошибка при формировании накладных.\nНе указано ЮЛ.\nДанные на кассу отправлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void CreateActPereoc()
        {
            Config.TTN = "";


            //пишем шапку
            idAll = Config.hCntMain.AddAllprihod(dtpDate.Value.Date, idTreq, ntypeorg);
            int CountPereocGoods = 0;

            //для каждого товара из списка
            for (int i = 0; dt.Rows.Count > i; i++)
            {
                int id_tov = int.Parse(dt.Rows[i]["id_tovar"].ToString());
                decimal curPrice = decimal.Parse(dt.Rows[i]["rcena"].ToString());
                decimal newPrice = decimal.Parse(dt.Rows[i]["zcenabnds"].ToString());
                string Ean = dt.Rows[i]["ean"].ToString();
                string Cname = dt.Rows[i]["cname"].ToString();
                string Npp = dt.Rows[i]["npp"].ToString();
                int id_req = int.Parse(dt.Rows[i]["id_req"].ToString());

                bool AnotherGoodPereocExists = Config.hCntMain.AnotherGoodPereocExists(
                    id_tov,
                    dtpDate.Value.Date,
                    curPrice,
                    newPrice);

                //если есть переоценка на эту дату с этим товаром
                if (AnotherGoodPereocExists)
                {
                    //запрос на добавление
                    if (MessageBox.Show("Для товара: " + Ean + " \"" + Cname + "\"\n"
                        + "на дату " + dtpDate.Value.Date.ToShortDateString()
                        + " уже имеется акт переоценки. \n"
                        + "Вы ходите добавить указанный товар в новый акт \nпереоценки?", "Создание акта переоценки",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        //сохраняем тело
                        //счетчик записанных записей в переоц
                        int idPereoc = Config.hCntMain.AddPereoc(idAll, id_tov, newPrice, curPrice, id_dep, Npp, id_req);
                        CountPereocGoods++;
                    }
                }
                else
                {
                    //сохраняем тело
                    //счетчик записанных записей в переоц
                    int idPereoc = Config.hCntMain.AddPereoc(idAll, id_tov, newPrice, curPrice, id_dep, Npp, id_req);
                    CountPereocGoods++;
                }
            }

            //если счетчик записей = 0, удаляем шапку
            if (CountPereocGoods == 0)
            {
                Config.hCntMain.DelAllprihod(idAll);
                MessageBox.Show("В акт переоценки не записан ни один товар!", "Создание акта переоценки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Акт переоценки успешно создан:\n"
                    + "Дата накладной: " + dtpDate.Value.Date.ToShortDateString() + "\n"
                    + "Номер заявки:   " + idTreq.ToString() + "\n"
                    + "Номер ТТН:      " + Config.TTN + "\n"
                    + "Тип накладной:  " + "\"Акт переоценки\"", "Создание акта переоценки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}

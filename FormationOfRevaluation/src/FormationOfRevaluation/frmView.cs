using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using numTextBox = Nwuram.Framework.fromElement.numTextBox;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace FormationOfRevaluation
{
    public partial class frmView : Form
    {
        
        BindingSource bsReq = new BindingSource();
        BindingSource bsReqDetails = new BindingSource();
        DataTable dtReq;
        DataTable dtReqDetails;
        int idCurTrequest = 0;

        bool actChanged = false;
        string mode = "view";


        public frmView()
        {            
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntMainKassRealizz == null)
                Config.hCntMainKassRealizz = new Procedures(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecondKassRealizz == null)
                Config.hCntSecondKassRealizz = new Procedures(ConnectionSettings.GetServer("4"), ConnectionSettings.GetDatabase("4"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        }

        private void frmView_Load(object sender, EventArgs e)
        {
            getShop();
            GetDeps();
            GetTypRequest();

            

            Config.CurDate = Config.hCntMain.getdate();
            dtpDateStart.Value = Config.CurDate.Date;
            dtpDateFinish.Value = Config.CurDate.Date;

            GetReq();
            grdReq_FiltersWidth();
            grdReqDetails_FiltersWidth();
            ButtonsEdit();

            getRcenaFuture();
        }

        /// <summary>
        /// Заполнение списка отделов
        /// </summary>
        private void GetDeps()
        {
            DataTable dtDeps = Config.hCntMain.GetDeps();
                
            if (dtDeps != null
                && dtDeps.Rows.Count > 0)
            {

                DataRow all = dtDeps.NewRow();
                all["name"] = "ВСЕ ОТДЕЛЫ";
                all["id"] = 0;
                dtDeps.Rows.InsertAt(all, 0);

                cbDepartment.DataSource = dtDeps;
                cbDepartment.DisplayMember = "name";
                cbDepartment.ValueMember = "id";
                cbDepartment.SelectedIndex = 0;

                cmbDeps.DataSource = dtDeps.Copy();
                cmbDeps.DisplayMember = "name";
                cmbDeps.ValueMember = "id";
                cmbDeps.SelectedIndex = 0;
            }
        }

        private void getShop()
        {
            DataTable dtShop = Config.hCntMain.getShop(false);

            cmbShop.DataSource = dtShop;
            cmbShop.DisplayMember = "cName";
            cmbShop.ValueMember = "id";

            cmbShopTab1.DataSource = dtShop.Copy();
            cmbShopTab1.DisplayMember = "cName";
            cmbShopTab1.ValueMember = "id";
        }

        private void GetTypRequest()
        {
            DataTable dtTypRequest = Config.hCntMain.GetTypRequest(); 

            if (dtTypRequest != null
                && dtTypRequest.Rows.Count > 0)
            {

                DataRow all = dtTypRequest.NewRow();
                all["name"] = "ВСЕ ТИПЫ";
                all["id"] = 1000;
                dtTypRequest.Rows.InsertAt(all, 0);

                cbTypeRequest.DataSource = dtTypRequest;
                cbTypeRequest.DisplayMember = "name";
                cbTypeRequest.ValueMember = "id";
                cbTypeRequest.SelectedIndex = 0;
            }
        }

        private void GetReq()
        {
            grdReq.DataSource = null;

            int dep = 0;
            int typeRequest = 1000;

            try { int.TryParse(cbDepartment.SelectedValue.ToString(), out dep); }
            catch { }
            try
            {
                if (cbTypeRequest.SelectedValue != null)
                    int.TryParse(cbTypeRequest.SelectedValue.ToString(), out typeRequest);
            }
            catch { }

            dtReq = new DataTable();
            bsReq = new BindingSource();

            if((int)cmbShopTab1.SelectedValue==1)
                dtReq = Config.hCntMain.GetReq(dtpDateStart.Value.Date, dtpDateFinish.Value.Date, dep, typeRequest); // add typeRequest and checkBox
            else
                dtReq = Config.hCntSecond.GetReq(dtpDateStart.Value.Date, dtpDateFinish.Value.Date, dep, typeRequest); // add typeRequest and checkBox

            if ((dtReq != null) && (dtReq.Rows.Count > 0))
            {
                SetFilter();
            }
            else
            {
                //если нет данных или ошибка получения данных
                EmptyGrid();
            }

            GetReqDetails();
            Filter();
        }

        private void SetFilter()
        {
            bsReq.DataSource = dtReq.DefaultView;
            grdReq.AutoGenerateColumns = false;
            grdReq.DataSource = bsReq;

            string filter = "";                      

            filter += (txtNumReq.Text.Trim().Length > 0
                ? (filter.Length > 0 ? " AND " : "") + "Convert(id,System.String) like '%" + EscapeLikeValue(txtNumReq.Text.Trim()) + "%'" 
                : "");
            
            bsReq.Filter = filter;
            
            Buttons();
        }

        private void EmptyGrid()
        {
            grdReq.AutoGenerateColumns = false;
            grdReq.DataSource = bsReq;
            bsReq.Filter = "0=1";
            Buttons();
        }

        private string EscapeLikeValue(string valueWithoutWildcards)
        {
            var sb = new StringBuilder();
            foreach (var c in valueWithoutWildcards)
            {
                switch (c)
                {
                    case ']':
                    case '[':
                    case '%':
                    case '*':
                        sb.Append("[").Append(c).Append("]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        private void grdReq_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            grdReq_FiltersWidth();
        }

        private void GetReqDetails()
        {
            grdReqDetails.DataSource = null;
            dtReqDetails = new DataTable();
            bsReqDetails = new BindingSource();

            if ((grdReq.Rows.Count > 0) && (grdReq.SelectedRows.Count > 0) && (grdReq.CurrentCell != null))
            {

                int id_treq = int.Parse(grdReq.Rows[grdReq.CurrentCell.RowIndex].Cells["id"].Value.ToString());


                

                dtReqDetails = Config.hCntMain.GetReqDetails(id_treq);

                if ((dtReqDetails != null) && (dtReqDetails.Rows.Count > 0))
                {
                    dtReqDetails.Columns["delete"].DefaultValue = 0;
                    dtReqDetails.Columns["id_tovar"].DefaultValue = 0;
                    SetFilterDetails();
                }
                else
                {
                    //если нет данных или ошибка получения данных
                    EmptyGridDetails();
                }
            }
        }

        private void SetFilterDetails()
        {
            
            bsReqDetails.DataSource = dtReqDetails.DefaultView;
            grdReqDetails.AutoGenerateColumns = false;
            grdReqDetails.DataSource = bsReqDetails;

            string filter = "";

            filter += (txtEan.Text.Trim().Length > 0
                ? (filter.Length > 0 ? " AND " : "") + "ean like '%" + EscapeLikeValue(txtEan.Text.Trim()) + "%'"
                : "");

            filter += (txtName.Text.Trim().Length > 0
                ? (filter.Length > 0 ? " AND " : "") + "cname like '%" + EscapeLikeValue(txtName.Text.Trim()) + "%'"
                : "");

            filter += (filter.Length > 0 ? " AND " : "") + "delete = 0";                

            bsReqDetails.Filter = filter;

            //Buttons();
        }

        private void EmptyGridDetails()
        {
            grdReqDetails.AutoGenerateColumns = false;
            grdReqDetails.DataSource = bsReqDetails;
            bsReqDetails.Filter = "0=1";
            //Buttons();
        }

        private void grdReqDetails_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {            
            grdReqDetails_FiltersWidth();
        }

        private void grdReq_FiltersWidth()
        {
            txtNumReq.Width = grdReq.Columns["id"].Width;
        }

        private void grdReqDetails_FiltersWidth()
        {
            txtEan.Location = new Point(grdReqDetails.Location.X + grdReqDetails.Columns["npp"].Width,
                txtEan.Location.Y);
            txtEan.Width = grdReqDetails.Columns["ean"].Width;
            txtName.Location = new Point(grdReqDetails.Location.X + grdReqDetails.Columns["npp"].Width + grdReqDetails.Columns["ean"].Width,
                txtName.Location.Y);
            txtName.Width = grdReqDetails.Columns["cname"].Width;
        }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDateStart.Value.Date > dtpDateFinish.Value.Date)
            {
                dtpDateFinish.Value = dtpDateStart.Value.Date;
            }

            GetReq();
        }

        private void dtpDateFinish_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDateStart.Value.Date > dtpDateFinish.Value.Date)
            {
                dtpDateStart.Value = dtpDateFinish.Value.Date;
            }

            GetReq();
        }

        private void cbDepartment_SelectedValueChanged(object sender, EventArgs e)
        {
            GetReq();
            dep_name.Visible = false;
            try
            {
                if ((cbDepartment.SelectedValue != null) && (int.Parse(cbDepartment.SelectedValue.ToString()) == 0))
                {
                    dep_name.Visible = true;
                }
            }
            catch { }
        }

        private void txtNumReq_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void txtEan_TextChanged(object sender, EventArgs e)
        {
            SetFilterDetails();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SetFilterDetails();
        }

        private void grdReq_SelectionChanged(object sender, EventArgs e)
        {
            if(dtReqDetails != null
                && dtReqDetails.Rows.Count > 0)
                idCurTrequest = (int)dtReqDetails.Select("id_treq <> 0")[0]["id_treq"];

            if (grdReq.SelectedRows.Count == 0
                ||
                (idCurTrequest != (int)grdReq.SelectedRows[0].Cells["id"].Value
                && actSaveDialogPassed()))
            {
                NewActSelect();
            }
            else
            {
                grdReq.CurrentCell = grdReq.Rows.Cast<DataGridViewRow>().Where(x => (int)x.Cells["id"].Value == idCurTrequest).First().Cells[0];
                GetReqDetails();
            }


        }

        private void NewActSelect()
        {
            GetReqDetails();
            Buttons();
            actChanged = false;
            rbView.Checked = true;
            mode = "view";
            zcenabnds.ReadOnly = true;
            ean.ReadOnly = true;
            grdReqDetails.AllowUserToAddRows = false;
            ButtonsEdit();
            grdReqDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (actSaveDialogPassed())
            {
                this.Close();
            }
        }

        private void btnCreateReval_Click(object sender, EventArgs e)
        {
            CreateReval(sender, e);
            Filter();
        }

        private void CreateReval (object sender, EventArgs e)
        {
            if ((grdReq.Rows.Count > 0) && (grdReq.SelectedRows.Count > 0) && (grdReq.CurrentCell != null))
            {
                    int id_Treq = int.Parse(grdReq.SelectedRows[0].Cells["id"].Value.ToString());
                    DateTime data_outt = DateTime.Parse(grdReq.SelectedRows[0].Cells["data_out"].Value.ToString());
                int ntype = 0;
                try
                {
                    ntype = int.Parse(grdReq.SelectedRows[0].Cells["ntypeorg"].Value.ToString());
                }
                catch { ntype = 0;}
                    int id_dep = int.Parse(grdReq.SelectedRows[0].Cells["dep_id"].Value.ToString());

                frmCreateReval frmCreate = new frmCreateReval(id_Treq, dtReqDetails, data_outt, ntype, id_dep);
                frmCreate.ShowDialog();
 
            }
            GetReq();
        }

        private void grdReq_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (grdReq.CurrentCell != null)
            {
                int emptyBuhFP = int.Parse(grdReq.Rows[e.RowIndex].Cells["emptyBuh"].Value.ToString());

                if (emptyBuhFP == 1)
                {
                    grdReq.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;                    
                }
                else
                {
                    grdReq.Rows[e.RowIndex].DefaultCellStyle.BackColor = pnProcessed.BackColor;
                }
            }

        }

        private void rbEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEdit.Checked)
            {
                mode = "edit";
                btnCreateReval.Enabled = false;
                zcenabnds.ReadOnly = false;
                ean.ReadOnly = false;
                
                grdReqDetails.Focus();
                grdReqDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Coral);
                grdReqDetails.CurrentCell = grdReqDetails.Rows[0].Cells["zcenabnds"];
                grdReqDetails.CurrentCell.Selected = true;
                grdReqDetails.AllowUserToAddRows = true;
            }
            else
            {
                if (actSaveDialogPassed())
                {
                    mode = "view";
                    NewActSelect();
                }
                else
                {
                    rbEdit.Checked = true;
                }
            }

            ButtonsEdit();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdReqDetails.CurrentRow != null)
            {
                if ((int)dtReqDetails.DefaultView[grdReqDetails.CurrentRow.Index]["id_req"] == 0)
                {
                    dtReqDetails.DefaultView[grdReqDetails.CurrentRow.Index].Delete();
                }
                else
                {
                    dtReqDetails.DefaultView[grdReqDetails.CurrentRow.Index]["delete"] = 1;
                }
                dtReqDetails.AcceptChanges();
            }
 
            SetFilterDetails();
            grdReqDetails.Refresh();
            actChanged = SomethingChanged();
            ButtonsEdit();
        }

        private void grdReqDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= EditingControl_KeyPress;
            e.Control.KeyPress -= Ean_KeyPress;

            if (e.Control is TextBox)
            {
                if (grdReqDetails.CurrentCell.OwningColumn.DisplayIndex == zcenabnds.Index)
                {
                    e.Control.KeyPress += EditingControl_KeyPress;
                }

                if (grdReqDetails.CurrentCell.OwningColumn.DisplayIndex == ean.Index)                    
                {
                    e.Control.KeyPress += Ean_KeyPress;
                }
            }
        }

        void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            bool integerr = false;
            bool negative = false;
                        

            if (!char.IsControl(e.KeyChar))
            {
                if (txt.SelectedText != "") { txt.Text.Replace(txt.SelectedText, ""); }

                char s = CorrectSymbols(txt, e.KeyChar, integerr, negative, 2);


                if (s != ' ')
                    e.KeyChar = s;
                else
                    e.Handled = true;
            }
        }

        void Ean_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar));
        }

        /// <summary>
        /// Процедура проверки введенного символа
        /// </summary>
        /// <param name="txt">число</param>
        /// <param name="ch">текущий вводимый символ</param>
        /// <param name="integerr">признак целого числа</param>
        /// <param name="negative">признак отрицательного числа</param>
        /// <param name="decimals">кол-во знаков после запятой</param>        
        /// <returns>символ после корректировки: если проверки пройдены возвращается тот же символ, что вводился, иначе возвращается ' '</returns>
        private char CorrectSymbols(TextBox txt, char ch, bool integerr, bool negative, int decimals)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

            if (integerr)
            {
                if (char.IsDigit(ch))
                {
                    return ch;
                }
            }
            else
            {
                //число
                if (char.IsDigit(ch))
                {
                    int sepIndex = txt.Text.IndexOf(separator());
                    int curIndex = txt.SelectionStart;
                    int count = 0;

                    //если разделитель не найден
                    if (sepIndex == -1)
                    {
                        return ch;
                    }
                    else
                    {
                        if (curIndex <= sepIndex)
                        {
                            return ch;
                        }
                        else
                        {
                            //кол-во знаков после запятой, которые уже заполнены
                            int countDecimal = txt.Text.Remove(0, sepIndex + 1).Trim().Length;
                            //ниже запрещаем добавлять цифры если уже есть нужное кол-во символов
                            //например если decimals = 2 и в поле уже есть 1,01 то запрещаем добавлять например 1,016
                            if (countDecimal < decimals)
                            {
                                return ch;
                            }
                        }                        
                    }
                }

                if (negative)
                {
                    if (
                        //знак минуса
                        (ch == '-')
                        &&
                        //первый
                        (txt.Text.Length == 0)
                        )
                    {
                        return ch;
                    }
                }

                if (
                    //символ разделитель
                    ((ch == '.') || (ch == ',') || (ch == '/') || (ch == 'ю') || (ch == 'б'))
                    &&
                    //не первый
                    (txt.Text.Length != 0)
                    &&
                    //других разделителей не было
                    (!AnotherSeparatorWasDetected(txt))
                    )
                {
                    return separator();
                }
            }

            return ' ';
        }

        /// <summary>
        /// Процедура получения текущего разделителя из региональных настроек
        /// </summary>
        /// <returns></returns>
        private char separator()
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            return char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        /// <summary>
        /// Процедура проверки наличия разделителя
        /// </summary>
        /// <param name="txt">число</param>
        /// <returns>true - разделитель уже есть, false - разделителя нет</returns>
        private bool AnotherSeparatorWasDetected(TextBox txt)
        {
            bool res = false;

            char[] dd = txt.Text.ToCharArray();

            for (int i = 0; dd.Count() > i; i++)
            {
                if (dd[i] == separator())
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        private void grdReqDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {            
            if (e.ColumnIndex == zcenabnds.Index)
            {
                decimal d = -1;

                decimal curPrice = 0;
                if(!decimal.TryParse(grdReqDetails.Rows[e.RowIndex].Cells["rcena"].Value.ToString(), out curPrice))
                {
                    return;
                }

                try
                {
                    d = decimal.Parse(e.FormattedValue.ToString());
                }
                catch { }
                

                if (d == 0)
                {
                    MessageBox.Show("Цена не может быть нулевой!", "Сообщение");
                    e.Cancel = true;
                    //return;
                }

                if (d == curPrice)
                {
                    MessageBox.Show("Новая и текущая цена не могут быть равны!", "Сообщение");
                    e.Cancel = true;
                }                
            }           
        }

        private void grdReqDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введите корректное значение цены!", "Сообщение");
        }

        private void grdReqDetails_CellValidated(object sender, DataGridViewCellEventArgs e)
        {            
            actChanged = SomethingChanged();
                
            ButtonsEdit();
        }

        /// <summary>
        /// Процедура определяет наличие изменений в акте(если менялась цена хотя бы у одного товара или товар был помечен на удаление)
        /// </summary>
        /// <returns>true - изменения произведены, false - изменений не было</returns>
        private bool SomethingChanged()
        {
            bool result = false;
            
            for (int i = 0; dtReqDetails.Rows.Count > i; i++)
            {               
                if (
                    decimal.Parse(dtReqDetails.Rows[i]["zcenabnds"].ToString())
                    != 
                    decimal.Parse(dtReqDetails.Rows[i]["zcenabndsOld"].ToString()))
                {
                    result = true;
                    break;
                }

                if (dtReqDetails.Rows[i]["delete"].ToString() == "1")
                {
                    result = true;
                    break;
                }

                if ((int)dtReqDetails.Rows[i]["id_req"] == 0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }


        private void Buttons()
        {
            Config.CurDate = Config.hCntMain.getdate();

            btnCreateReval.Enabled = false;
            rbView.Enabled = false;
            rbEdit.Enabled = false;            

            if ((grdReq.Rows.Count > 0) && (grdReq.CurrentCell != null))
            {
                int emptyBuhBT = int.Parse(grdReq.Rows[grdReq.CurrentCell.RowIndex].Cells["emptyBuh"].Value.ToString());
                DateTime data_outBT = DateTime.Parse(grdReq.Rows[grdReq.CurrentCell.RowIndex].Cells["data_out"].Value.ToString());

                if (emptyBuhBT == 1)
                {
                    rbView.Enabled = true;
                    rbEdit.Enabled = true;                                        
                }

                if (emptyBuhBT == 1) 
                    //&& (data_outBT.Date >= Config.CurDate))
                {
                    btnCreateReval.Enabled = true;
                }
            }
        }

        //процедура определения доступности кнопок удаления товара из акта и сохранения акта после редактирования
        private void ButtonsEdit()
        {
            btnDel.Enabled = false;
            btnSave.Enabled = false;

            if (mode == "edit")
            {
                if (actChanged)
                {
                    btnSave.Enabled = true;
                }

                if (CountNotDeletedGoods() > 1)
                {
                    btnDel.Enabled = true;
                }
            }
        }

        //процедура считает сколько не удаленных товаров осталось в акте
        private int CountNotDeletedGoods()
        {
            int count = 0;

            for (int i = 0; dtReqDetails.Rows.Count > i; i++)
            {
                if (dtReqDetails.Rows[i]["delete"].ToString() == "0")
                {
                    count++;
                }
            }

            return count;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (canSave())
            {
                save();
                MessageBox.Show("Данные сохранены!", "Сообщение");
                NewActSelect();
                ButtonsEdit();
            }
        }

        private void save()
        {            
            string comment = "Редактирование содержания акта переоценки";

            Logging.StartFirstLevel(27);
            Logging.Comment("Начало операции \"" + comment + "\"");
            Logging.Comment("id акта = " + dtReqDetails.Rows[0]["id_treq"].ToString());            
            Logging.Comment("");

            int id_trequest = int.Parse(dtReqDetails.Select("id_treq <> 0")[0]["id_treq"].ToString());

            for (int i = 0; dtReqDetails.Rows.Count > i; i++)
            {
                int s_id_req = int.Parse(dtReqDetails.Rows[i]["id_req"].ToString());
                decimal s_zcenaAfter = decimal.Parse(dtReqDetails.Rows[i]["zcenabnds"].ToString());
                decimal s_zcenaBefore = decimal.Parse(dtReqDetails.Rows[i]["zcenabndsOld"].ToString());
                int s_delete = int.Parse(dtReqDetails.Rows[i]["delete"].ToString());
                
                //если изменилась цена или товар в акте был помечен на удаление
                if ((s_zcenaAfter != s_zcenaBefore)
                    || 
                    (s_delete == 1))
                {
                    if (s_id_req != 0)
                    {
                        if (s_delete == 1)
                        {
                            Logging.Comment("Удаление записи, id = " + s_id_req.ToString()
                                + ", npp = "
                                + dtReqDetails.Rows[i]["npp"].ToString()
                                + ", id_tovar = "
                                + dtReqDetails.Rows[i]["id_tovar"].ToString()
                                + ", EAN = "
                                + dtReqDetails.Rows[i]["ean"].ToString()
                                + ", Наименование товара \""
                                + dtReqDetails.Rows[i]["cname"].ToString()
                                + "\", Кол-во = "
                                + dtReqDetails.Rows[i]["netto"].ToString()
                                + ", zcenabnds = "
                                + dtReqDetails.Rows[i]["zcenabnds"].ToString()
                                + ", rcena = "
                                + dtReqDetails.Rows[i]["rcena"].ToString()
                                );
                        }
                        else
                        {
                            Logging.Comment("Редактирование записи, id = " + s_id_req.ToString()
                                + ", npp = "
                                + dtReqDetails.Rows[i]["npp"].ToString()
                                + ", id_tovar = "
                                + dtReqDetails.Rows[i]["id_tovar"].ToString()
                                + ", EAN = "
                                + dtReqDetails.Rows[i]["ean"].ToString()
                                + ", Наименование товара \""
                                + dtReqDetails.Rows[i]["cname"].ToString()
                                + "\", Кол-во = "
                                + dtReqDetails.Rows[i]["netto"].ToString()                            
                                + ", rcena = "
                                + dtReqDetails.Rows[i]["rcena"].ToString()
                                );
                            Logging.VariableChange("Новая цена = ", s_zcenaAfter, s_zcenaBefore, typeLog._decimal);
                        }
                        
                            Config.hCntMain.UpdateReq(s_id_req, s_zcenaAfter, s_delete);
                    }
                    else
                    {
                        Config.hCntMain.UpdateReq(s_id_req,
                                                    s_zcenaAfter,
                                                    id_trequest,
                                                    (int)dtReqDetails.Rows[i]["id_tovar"],
                                                    (decimal)dtReqDetails.Rows[i]["netto"],
                                                    (decimal)dtReqDetails.Rows[i]["rcena"]);
                    }
                }
            }

            Logging.Comment("");
            Logging.Comment("Завершение операции \"" + comment + "\"");
            Logging.StopFirstLevel();
        }

        private void grdReqDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (
                (grdReqDetails.Rows[e.RowIndex].Cells["id_tovar"].Value == null)
                ||
                (e.ColumnIndex == zcenabnds.Index 
                && (int)grdReqDetails.Rows[e.RowIndex].Cells["id_tovar"].Value == 0)
                ||
                (e.ColumnIndex == ean.Index
                && (int)grdReqDetails.Rows[e.RowIndex].Cells["id_tovar"].Value != 0)
               )
            {
                e.Cancel = true;
            }
        }

        private void grdReqDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ean.Index
                && grdReqDetails.Rows[e.RowIndex].Cells["id_tovar"].Value != null
                && (int)grdReqDetails.Rows[e.RowIndex].Cells["id_tovar"].Value == 0)
            {
                grdReqDetails.EndEdit();
                if (grdReqDetails.Rows[e.RowIndex].Cells["ean"].EditedFormattedValue != null
                    && grdReqDetails.Rows[e.RowIndex].Cells["ean"].EditedFormattedValue != DBNull.Value
                    && grdReqDetails.Rows[e.RowIndex].Cells["ean"].EditedFormattedValue.ToString().Trim().Length >= 3)
                {
                    //Поиск товара по коду
                    string curEan = grdReqDetails.Rows[e.RowIndex].Cells["ean"].EditedFormattedValue.ToString().Trim();

                    if (dtReqDetails.AsEnumerable().Where(x => x["ean"].ToString().Trim() == curEan && (int)x["id_tovar"] != 0).Count() > 0)
                    {
                        MessageBox.Show("Товар с данным кодом\nуже добавлен в акт.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //Данные по товару
                        DataTable dtGoodInfo = Config.hCntMain.GetGoodByEan(curEan,
                                                                            (int)grdReq.CurrentRow.Cells["id"].Value);
                        if (dtGoodInfo == null
                            || dtGoodInfo.Rows.Count == 0)
                        {
                            MessageBox.Show("Ошибка при поиске товара.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            grdReqDetails.AllowUserToAddRows = false;
                            if (dtGoodInfo.Columns.Contains("res"))
                            {
                                if ((int)dtGoodInfo.Rows[0]["res"] == 0)
                                {
                                    MessageBox.Show("Товар с данным кодом\nне найден.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                if ((int)dtGoodInfo.Rows[0]["res"] == 1)
                                {
                                    MessageBox.Show("Товар с данным кодом\nпринадлежит другому отделу.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                if ((int)dtGoodInfo.Rows[0]["res"] == 2)
                                {
                                    MessageBox.Show("Товар с данным кодом\nявляется резервным.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                //Добавление в таблицу с телом акта
                                DataRow drEdited = dtReqDetails.NewRow();
                                dtGoodInfo.Rows[0]["npp"] = int.Parse(dtReqDetails.AsEnumerable()
                                                                        .Where(x => x["npp"] is decimal)
                                                                        .Max(x => x["npp"]).ToString()) + 1;
                                for (int i = 0; i < dtReqDetails.Columns.Count; i++)
                                {
                                    if (dtGoodInfo.Columns.Contains(dtReqDetails.Columns[i].ColumnName))
                                    {
                                        drEdited[i] = dtGoodInfo.Rows[0][dtReqDetails.Columns[i].ColumnName];
                                    }
                                }
                                dtReqDetails.Rows.Add(drEdited);
                            }

                            grdReqDetails.AllowUserToAddRows = true;
                        }
                    }
                }

                //Удаление строки для которой не найдено информации по товару
                var goods = dtReqDetails.DefaultView.Cast<DataRowView>().Where(x => (int)x["id_tovar"] == 0);
                if (goods.Count() != 0)
                {
                    goods.First().Delete();
                }
                grdReqDetails.Refresh();

                actChanged = SomethingChanged();
                ButtonsEdit();
            }
        }

        /// <summary>
        /// Определяет, можно ли сохранить акт
        /// </summary>
        /// <returns>Результат проверки</returns>
        private bool canSave()
        {
            bool canSave = true;
            if (dtReqDetails.Select("zcenabnds = 0").Count() > 0)
            {
                MessageBox.Show("В акте присутствуют товары\nс нулевой новой ценой.\nСохранение невозможно.",
                                "Сообщение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                canSave = false;
            }
            return canSave;
        }

        /// <summary>
        ///  Обработка несохраненных данных
        /// </summary>
        /// <returns>Был ли сохранен акт</returns>
        private bool actSaveDialogPassed()
        {
            if ((mode == "edit") && (actChanged))
            {
                if (MessageBox.Show("Сохранить внесенные изменения?", "Запрос",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (canSave())
                    {
                        //сохранение
                        save();
                        MessageBox.Show("Данные сохранены!", "Сообщение");
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void cbProcessed_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbProcessed.Checked == true && grdReq.Rows.Count > 0)
            //{
                Filter();
                //grdReq.CurrentCell = null;
                //for (int i = 0; i < grdReq.Rows.Count; i++)
                //    grdReq.Rows[i].Visible = int.Parse(grdReq.Rows[i].Cells["emptyBuh"].Value.ToString()) == 1;
            //}
            //else
            //    GetReq();

        }


        private void grdReq_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grdReq.CurrentCell != null && e.RowIndex >= 0)
                checkTypeProperties(sender, e);
        }

        private void checkTypeProperties(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grdReq.Rows[e.RowIndex].Cells["TypeProperties"].Value == null) return;
            if (grdReq.Rows[e.RowIndex].Cells["TypeProperties"].Value.ToString().Equals("плановый"))
            {
                lbDateStartPrise.Text = grdReq.Rows[e.RowIndex].Cells["DateStart"].Value.ToString();
                lbDateStartPrise.Visible = labelDateStartPrise.Visible = true;
            }
            else lbDateStartPrise.Visible = labelDateStartPrise.Visible = false;

        }

        private void cbTypeRequest_SelectedValueChanged(object sender, EventArgs e)
        {
             GetReq();
        }

        private void Filter()
        {
            if (cbProcessed.Checked == true && grdReq.Rows.Count > 0)
            {
                bsReq.Filter = "emptyBuh = 1";
            }
            else
                bsReq.Filter = "";


        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cEan.Name))
                {
                    tbEan.Location = new Point(dgvData.Location.X + 1 + width, tbEan.Location.Y);
                    tbEan.Size = new Size(cEan.Width, tbEan.Size.Height);
                }

                if (col.Name.Equals(cNameTab2.Name))
                {
                    tbName.Location = new Point(dgvData.Location.X + 1 + width, tbEan.Location.Y);
                    tbName.Size = new Size(cNameTab2.Width, tbEan.Size.Height);
                }

                width += col.Width;
            }
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getRcenaFuture();
        }

        private void dtpDateTab2_CloseUp(object sender, EventArgs e)
        {
            getRcenaFuture();
        }

        private void dtpDateTab2_Leave(object sender, EventArgs e)
        {
            getRcenaFuture();
        }

        private void btCloseTab2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private DataTable dtRcenaFuture;
        private void getRcenaFuture()
        {
            DateTime date = dtpDateTab2.Value.Date;

            DateTime nowDate =  Config.hCntMain.getdate();

            btSend.Visible =  date.Date == nowDate.Date;

            btDel.Visible = date.Date != nowDate.Date;

            if ((int)cmbShop.SelectedValue == 1)
                dtRcenaFuture = Config.hCntMain.getRcenaFuture(date);
            else
                dtRcenaFuture = Config.hCntSecond.getRcenaFuture(date);
            setFilter();
            dgvData.DataSource = dtRcenaFuture;
        }

        private void tbEan_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            if (dtRcenaFuture == null || dtRcenaFuture.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;
                btPrint.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbEan.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"ean like '%{tbEan.Text.Trim()}%'";

                if (tbName.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cname like '%{tbName.Text.Trim()}%'";

                if ((Int16)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_otdel  = {cmbDeps.SelectedValue}";

                dtRcenaFuture.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtRcenaFuture.DefaultView.RowFilter = "id_tovar = -1";
            }
            finally
            {
                btPrint.Enabled =
                dtRcenaFuture.DefaultView.Count != 0;
                //dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (cV.Index == e.ColumnIndex)
            {
                dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = new List<int>(new int[] { 1, 3 }).Contains((int)dtRcenaFuture.DefaultView[e.RowIndex]["ntypetovar"]) || dtRcenaFuture.DefaultView[e.RowIndex]["idPromo"] != DBNull.Value;
            }
        }

        private void dgvData_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtRcenaFuture == null || dtRcenaFuture.Rows.Count == 0) return;
            if (e.ColumnIndex == cV.Index)
            {
                EnumerableRowCollection<DataRow> rowCollect = dtRcenaFuture.AsEnumerable()
                        .Where(r => r.Field<bool>("isSelect"));

                if (rowCollect.Count() > 0)
                {
                    foreach (DataRow row in rowCollect)
                        row["isSelect"] = false;

                    dtRcenaFuture.AcceptChanges();
                }
                else
                {
                    foreach (DataRow row in dtRcenaFuture.Rows)
                    {
                        if (new List<int>(new int[] { 1, 3 }).Contains((int)row["ntypetovar"])) continue;
                        if (row["idPromo"] != DBNull.Value) continue;
                        row["isSelect"] = true;
                    }

                    dtRcenaFuture.AcceptChanges();
                }
            }
        }

        private void btDropAll_Click(object sender, EventArgs e)
        {
            if (dtRcenaFuture == null || dtRcenaFuture.Rows.Count == 0) return;

            EnumerableRowCollection<DataRow> rowCollect = dtRcenaFuture.AsEnumerable()
                    .Where(r => r.Field<bool>("isSelect"));

            if (rowCollect.Count() > 0)
            {
                foreach (DataRow row in rowCollect)
                    row["isSelect"] = false;

                dtRcenaFuture.AcceptChanges();
            }
        }


        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible && !col.Name.Equals(cV.Name))
                {
                    maxColumns++;
                    if (col.Name.Equals(cDeps.Name)) setWidthColumn(indexRow, maxColumns, 13, report);
                    if (col.Name.Equals(cEan.Name)) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals(cNameTab2.Name)) setWidthColumn(indexRow, maxColumns, 40, report);
                    if (col.Name.Equals(cPrice.Name)) setWidthColumn(indexRow, maxColumns, 11, report);
                }


            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{this.Text}", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;


            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Дата: {dtpDateTab2.Value.ToShortDateString()}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Магазин: {cmbShop.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отдел: {cmbDeps.Text}", indexRow, 1);
            indexRow++;

            //report.Merge(indexRow, 1, indexRow, maxColumns);
            //report.AddSingleValue($"Должность: {cmbPost.Text}", indexRow, 1);
            //indexRow++;

            //report.Merge(indexRow, 1, indexRow, maxColumns);
            //report.AddSingleValue($"Место работы: {(rbOffice.Checked ? rbOffice.Text : rbUni.Text)}", indexRow, 1);
            //indexRow++;

            //report.Merge(indexRow, 1, indexRow, maxColumns);
            //report.AddSingleValue($"Статус сотрудника: {(rbWork.Checked ? rbWork.Text : rbUnemploy.Text)}", indexRow, 1);
            //indexRow++;

            if (tbEan.Text.Trim().Length != 0 || tbName.Text.Trim().Length != 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтр: {(tbEan.Text.Trim().Length != 0 ? $"EAN:{tbEan.Text.Trim()} | " : "")} {(tbName.Text.Trim().Length != 0 ? $"Наименование:{tbName.Text.Trim()}" : "")}", indexRow, 1);
                indexRow++;
            }

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            int indexCol = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
                if (col.Visible && !col.Name.Equals(cV.Name))
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            foreach (DataRowView row in dtRcenaFuture.DefaultView)
            {
                indexCol = 1;
                report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (col.Visible && !col.Name.Equals(cV.Name))
                    {
                        if (row[col.DataPropertyName] is DateTime)
                            report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                        else
                           if (row[col.DataPropertyName] is decimal || row[col.DataPropertyName] is double)
                        {
                            report.AddSingleValueObject(row[col.DataPropertyName], indexRow, indexCol);
                            report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                        }
                        else
                            report.AddSingleValue(row[col.DataPropertyName].ToString(), indexRow, indexCol);

                        indexCol++;
                    }
                }
             
                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);

                indexRow++;
            }           

            report.Show();
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (dtRcenaFuture == null || dtRcenaFuture.Rows.Count == 0) return;

            EnumerableRowCollection<DataRow> rowCollect = dtRcenaFuture.AsEnumerable()
                    .Where(r => r.Field<bool>("isSelect"));

            if (rowCollect.Count() > 0)
            {
                DateTime date = dtpDateTab2.Value.Date;

                foreach (DataRow row in rowCollect)
                {
                    decimal tmpDec = ((decimal)row["rcena"] * 100);
                    int newPrice = decimal.ToInt32(tmpDec);

                    if ((int)cmbShop.SelectedValue == 1)
                    {
                        Config.hCntMain.setRcenaFuture((int)row["id_tovar"], date, (decimal)row["rcena"], false);
                        Config.hCntMainKassRealizz.setGoodsUpdate((int)row["id_otdel"], (int)row["nds"], (int)row["id_grp1"], (int)row["ntypeorg"], newPrice, (string)row["cname"], (string)row["ean"]);
                    }
                    else
                    {
                        Config.hCntSecond.setRcenaFuture((int)row["id_tovar"], date, (decimal)row["rcena"], false);
                        Config.hCntSecondKassRealizz.setGoodsUpdate((int)row["id_otdel"], (int)row["nds"], (int)row["id_grp1"], (int)row["ntypeorg"], newPrice, (string)row["cname"], (string)row["ean"]);
                    }
                }

                getRcenaFuture();
            }
            //
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dtRcenaFuture == null || dtRcenaFuture.Rows.Count == 0) return;

            EnumerableRowCollection<DataRow> rowCollect = dtRcenaFuture.AsEnumerable()
                    .Where(r => !r.Field<bool>("isSelect"));

            if (rowCollect.Count() > 0)
            {
                DateTime date = dtpDateTab2.Value.Date;

                foreach (DataRow row in rowCollect)
                {
                    Config.hCntMain.setRcenaFuture((int)row["id_tovar"], date, (decimal)row["rcena"], true);
                }

                getRcenaFuture();
            }
        }

        private void cmbShopTab1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetReq();
        }
    }
}

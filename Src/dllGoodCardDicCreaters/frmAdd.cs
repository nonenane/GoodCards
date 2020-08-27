using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllGoodCardDicCreaters
{
    public partial class frmAdd : Form
    {
        public DataRowView row { set; private get; }

        private bool isEditData = false;
        private string oldName,oldCode;
        private int id = 0;        
        private DataTable dtAdress;


        public frmAdd()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Сохранить");
            dgvAdress.AutoGenerateColumns = false;
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            init_combobox();
            if (row != null)
            {
                id = (int)row["id"];
                tbName.Text = (string)row["cName"];
                oldName = tbName.Text.Trim();

                tbCode.Text = (string)row["inn"];
                oldCode = tbName.Text.Trim();

                cmbTypeSubject.SelectedValue = row["id_type_org"];
                getAdresProizvod(id);
            }
            else
            {
                btAdd.Visible = false;
                btEdit.Visible = false;
                btDelete.Visible = false;
                getAdresProizvod(id);
            }

            isEditData = false;
        }

        private void getAdresProizvod(int id_proizvoditel)
        {
            Task<DataTable> task = Config.hCntMain.getAdressProizvodVsProizvod(id_proizvoditel);
            task.Wait();
            dtAdress = task.Result;
            dgvAdress.DataSource = task.Result;
            dgvAdress_SelectionChanged(null, null);
        }

        private void init_combobox()
        {
            Task<DataTable> task = Config.hCntMain.getTypeOrg();
            task.Wait();

            cmbTypeSubject.DataSource = task.Result;
            cmbTypeSubject.DisplayMember = "cName";
            cmbTypeSubject.ValueMember = "id";
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lName.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            Task<DataTable> task = Config.hCntMain.setProizvoditel(id, tbName.Text,tbCode.Text,(int)cmbTypeSubject.SelectedValue, true, false, 0);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует должность с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая ***.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            task = Config.hCntSecond.setProizvoditel(id, tbName.Text, tbCode.Text, (int)cmbTypeSubject.SelectedValue, true, false, 0);
            task.Wait();

            dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует должность с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая ***.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (id == 0)
            {
                id = (int)dtResult.Rows[0]["id"];
                Logging.StartFirstLevel(1564);
                //Logging.Comment("Добавить Тип документа");
                Logging.Comment($"ID: {id}");
                Logging.Comment($"Наименование: {tbName.Text.Trim()}");
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1565);
                //Logging.Comment("Редактировать Тип документа");
                Logging.Comment($"ID: {id}");
                Logging.VariableChange("Наименование", tbName.Text.Trim(), oldName);
                Logging.StopFirstLevel();
            }

            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void btDicSubject_Click(object sender, EventArgs e)
        {
            new dllGoodCardDicTypeOwnership.frmList().ShowDialog();
            init_combobox();
        }

        private void dgvAdress_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAdress.CurrentRow == null || dgvAdress.CurrentRow.Index == -1 || dtAdress == null || dtAdress.DefaultView.Count == 0 || dgvAdress.CurrentRow.Index >= dtAdress.DefaultView.Count)
            {
                btDelete.Enabled = false;
                btEdit.Enabled = false;                
                return;
            }
            btDelete.Enabled = true;
            btEdit.Enabled = true;
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvAdress.CurrentRow != null && dgvAdress.CurrentRow.Index != -1 && dtAdress != null && dtAdress.DefaultView.Count != 0)
            {
                DataRowView row = dtAdress.DefaultView[dgvAdress.CurrentRow.Index];
                if (DialogResult.OK == new frmAddAdres() { Text = "Редактировать адрес производителя", row = row }.ShowDialog())
                    getAdresProizvod(id);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmAddAdres() { Text = "Добавить производителя",id_proizvoditel = id }.ShowDialog())
                getAdresProizvod(id);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvAdress.CurrentRow != null && dgvAdress.CurrentRow.Index != -1 && dtAdress != null && dtAdress.DefaultView.Count != 0)
            {
                int id = (int)dtAdress.DefaultView[dgvAdress.CurrentRow.Index]["id"];
                int id_proizvoditel = (int)dtAdress.DefaultView[dgvAdress.CurrentRow.Index]["id_proizvoditel"];
                int id_subject = (int)dtAdress.DefaultView[dgvAdress.CurrentRow.Index]["id_subject"];
                bool isActive = (bool)dtAdress.DefaultView[dgvAdress.CurrentRow.Index]["isActive"];
                string cName = (string)dtAdress.DefaultView[dgvAdress.CurrentRow.Index]["street"];

                Task<DataTable> task = Config.hCntMain.setAdresProizvod(id,id_proizvoditel,id_subject,cName, isActive, true, 0);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Config.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getAdresProizvod(id);
                    return;
                }


                task = Config.hCntSecond.setAdresProizvod(id, id_proizvoditel, id_subject, cName, isActive, true, 0);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int _result = (int)task.Result.Rows[0]["id"];

                if (_result == -1)
                {
                    MessageBox.Show(Config.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getAdresProizvod(id);
                    return;
                }




                if (result == -2 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show(Config.centralText("Выбранная для удаления запись используется в программе.\nСделать запись недействующей?\n"), "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        //setLog(id, 1542);
                        task = Config.hCntMain.setAdresProizvod(id, id_proizvoditel, id_subject, cName, !isActive, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        task = Config.hCntSecond.setAdresProizvod(id, id_proizvoditel, id_subject, cName, !isActive, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        getAdresProizvod(id);
                        return;
                    }
                }
                else
                if (result == 0 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        //setLog(id, 1566);
                        task = Config.hCntMain.setAdresProizvod(id, id_proizvoditel, id_subject, cName, isActive, true, 1);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        task = Config.hCntSecond.setAdresProizvod(id, id_proizvoditel, id_subject, cName, isActive, true, 1);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        getAdresProizvod(id);
                        return;
                    }
                }
                else if (!isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        //setLog(id, 1543);
                        task = Config.hCntMain.setAdresProizvod(id, id_proizvoditel, id_subject, cName, !isActive, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        task = Config.hCntSecond.setAdresProizvod(id, id_proizvoditel, id_subject, cName, !isActive, false, 0);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        getAdresProizvod(id);
                        return;
                    }
                }
            }
        }

        private void tbCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }
    }
}

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

namespace dllGoodCardDicTuGrp
{
    public partial class frmAdd : Form
    {
        public DataRowView row { set; private get; }

        private bool isEditData = false;
        private string oldName,oldCode;
        private int id = 0;        
        private DataTable dtGrp2,dtGrp_old;


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
                                               
                cmbDeps.SelectedValue = row["id_otdel"];
                cmbDeps.Enabled = false;
                cmbNds.SelectedValue = row["id_nds"];

                if (dtGrp2 != null)
                {
                    Task<DataTable> task = Config.hCntMain.getGrp1VsGrp2(id);
                    task.Wait();
                    dtGrp_old = task.Result;
                    foreach (DataRow row in dtGrp_old.Rows)
                    {
                        EnumerableRowCollection<DataRow> rowCollect = dtGrp2.AsEnumerable().Where(r => r.Field<int>("id") == (int)row["id"]);
                        if (rowCollect.Count() > 0)
                        {
                            rowCollect.First()["isSelect"] = true;
                        }
                    }
                }

                getAdresProizvod((int)cmbDeps.SelectedValue);
            }
            else
            {                
                getAdresProizvod(0);
            }

            isEditData = false;
        }

        private void getAdresProizvod(int id_deps)
        {
            if (dtGrp2 == null) { dgvAdress.DataSource = null; return; }

            string filter  = $"id_otdel = {id_deps}";
            if (tbNaneGrp.Text.Trim().Length != 0)
                filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbNaneGrp.Text.Trim()}%'";

            dtGrp2.DefaultView.RowFilter = filter;
            dtGrp2.DefaultView.Sort = "cName asc";

            dgvAdress.DataSource = dtGrp2;

            //Task <DataTable> task = Config.hCntMain.getGrp1VsGrp2(id_proizvoditel);
            //task.Wait();
            //dtAdress = task.Result;
            //dgvAdress.DataSource = task.Result;
            
        }

        private void init_combobox()
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(false);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";
            cmbDeps.DataSource = dtObjectLease;
            cmbDeps.SelectedIndex = -1;

            task = Config.hCntMain.getNds(false);
            task.Wait();
            DataTable dtNds = task.Result;

            cmbNds.DisplayMember = "cName";
            cmbNds.ValueMember = "id";
            cmbNds.DataSource = dtNds;
            cmbNds.SelectedIndex = -1;

            task = Config.hCntMain.getGrp2();
            task.Wait();            
            dtGrp2 = task.Result;
            if (dtGrp2 != null)
            {
                if (!dtGrp2.Columns.Contains("isSelect"))
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = "isSelect";
                    col.DataType = typeof(bool);
                    col.DefaultValue = false;
                    dtGrp2.Columns.Add(col);
                }
            }
            getAdresProizvod(0);
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
            if (cmbNds.SelectedValue == null)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{label2.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbNds.Focus();
                return;
            }

            if (cmbDeps.SelectedValue == null)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{label2.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeps.Focus();
                return;
            }

            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lName.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            EnumerableRowCollection<DataRow> rowCollect = dtGrp2.AsEnumerable().Where(r => r.Field<bool>("isSelect") && r.Field<int>("id_otdel") == (int)cmbDeps.SelectedValue);
            //if (rowCollect.Count() == 0)
            //{                
                //return;
            //}

            //Task<DataTable> task = Config.hCntMain.setProizvoditel(id, tbName.Text,tbCode.Text,(int)cmbTypeSubject.SelectedValue, true, false, 0,false);
            //task.Wait();

            DataTable dtResult = new DataTable();// task.Result;

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

            id = (int)dtResult.Rows[0]["id"];

            //task = Config.hCntSecond.setProizvoditel(id, tbName.Text, tbCode.Text, (int)cmbTypeSubject.SelectedValue, true, false, 0, true);
            //task.Wait();

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

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getAdresProizvod((int)cmbDeps.SelectedValue);
        }

        private void tbNaneGrp_TextChanged(object sender, EventArgs e)
        {
            if (cmbDeps.SelectedIndex == -1) return;

            getAdresProizvod((int)cmbDeps.SelectedValue);
        }

        private void dgvAdress_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbNaneGrp.Location = new Point(dgvAdress.Location.X+1+cV.Width, tbNaneGrp.Location.Y);
            tbNaneGrp.Size = new Size(cNameGrp.Width, tbNaneGrp.Height);
        }


    }
}

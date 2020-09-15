using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllGoodCardDicTuGrp
{
    public partial class frmList : Form
    {
        private DataTable dtData;
        public frmList()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            
            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;
            dgvAdress.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btDelete, "Удалить");
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(button1, "Просмотр менеджеров ТУ группы");

            btAdd.Visible = btEdit.Visible = btDelete.Visible = new List<string> { "ИНФ", "СОП" }.Contains(UserSettings.User.StatusCode);
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";
            cmbDeps.DataSource = dtObjectLease;

            task = Config.hCntMain.getNTypeOrg(true);
            task.Wait();
            DataTable dtNtypeOrg = task.Result;

            cmbUL.DisplayMember = "cName";
            cmbUL.ValueMember = "id";
            cmbUL.DataSource = dtNtypeOrg;

            get_data();
        }

        private void frmList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmAdd() { Text = "Добавить ТУ группу" }.ShowDialog())
                get_data();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (DialogResult.OK == new frmAdd() { Text = "Редактировать ТУ группу", row = row }.ShowDialog())
                    get_data();
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                int id = (int)row["id"];
                string cName = (string)row["cName"];
                int id_otdel = (int)row["id_otdel"];
                int id_nds = (int)row["id_nds"];
                int? ntypeorg = null;
                if (row["ntypeorg"] != DBNull.Value) ntypeorg = (int)row["ntypeorg"];
                bool isCredit = (bool)row["isCredit"];
                bool isWithSubGroups = (int)row["isWithSubGroups"]==1;
                bool isActive = (bool)row["isActive"];
                bool isDel = true;
                int result = 0;
                bool isAutoIncriments = false;

                Task<DataTable> task = Config.hCntMain.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, isActive, isDel, result, isAutoIncriments);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Config.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    get_data();
                    return;
                }

                if (result == -2 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show(Config.centralText("Выбранная для удаления запись используется в программе.\nСделать запись недействующей?\n"), "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 1542);
                        //task = Config.hCntMain.setProizvoditel(id, cName, code, id_type_org, !isActive, false, 0, false);
                        result = 1;
                        isDel = false;
                        task = Config.hCntMain.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, !isActive, isDel, result, isAutoIncriments);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //task = Config.hCntSecond.setProizvoditel(id, cName, code, id_type_org, !isActive, false, 0, true);
                        result = 1;
                        isDel = false;
                        isAutoIncriments = true;
                        task = Config.hCntSecond.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, !isActive, isDel, result, isAutoIncriments);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        get_data();
                        return;
                    }
                }
                else
                if (result == 0 && isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 1566);
                        //task = Config.hCntMain.setProizvoditel(id, cName, code, id_type_org, isActive, true, 1, false);                        
                        //isAutoIncriments = true;
                        isDel = true;
                        result = 1;
                        task = Config.hCntMain.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, isActive, isDel, result, isAutoIncriments);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //task = Config.hCntSecond.setProizvoditel(id, cName, code, id_type_org, isActive, true, 1, true);
                        isAutoIncriments = true;
                        isDel = true;
                        result = 1;
                        task = Config.hCntSecond.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, isActive, isDel, result, isAutoIncriments);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        get_data();
                        return;
                    }
                }
                else if (!isActive)
                {
                    if (DialogResult.Yes == MessageBox.Show("Сделать выбранную запись действующей?", "Восстановление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        setLog(id, 1543);
                        //task = Config.hCntMain.setProizvoditel(id, cName, code, id_type_org, !isActive, false, 0, false);
                        //isAutoIncriments = true;
                        result = 1;
                        isDel = false;
                        task = Config.hCntMain.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, !isActive, isDel, result, isAutoIncriments);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        //task = Config.hCntSecond.setProizvoditel(id, cName, code, id_type_org, !isActive, false, 0, true);
                        isAutoIncriments = true;
                        result = 1;
                        isDel = false;
                        task = Config.hCntSecond.setGrp1(id, cName, id_otdel, id_nds, ntypeorg, isCredit, isWithSubGroups, !isActive, isDel, result, isAutoIncriments);
                        task.Wait();
                        if (task.Result == null)
                        {
                            MessageBox.Show(Config.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        get_data();
                        return;
                    }
                }
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void chbNotActive_CheckedChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btEdit.Enabled = btDelete.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbName.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbName.Text.Trim()}%'";

                if ((int)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_otdel = {cmbDeps.SelectedValue}";

                if ((int)cmbUL.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"ntypeorg = {cmbUL.SelectedValue}";

                if (!chbNotActive.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                if(chbIsCredit.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isCredit = 1";

                if (chbWithSubGroups.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isWithSubGroups = 1";

                dtData.DefaultView.RowFilter = filter;
                dtData.DefaultView.Sort = "id_otdel asc, cName asc";
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btEdit.Enabled = btDelete.Enabled =
                dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0 || dgvData.CurrentRow.Index >= dtData.DefaultView.Count)
            {
                btDelete.Enabled = false;
                btEdit.Enabled = false;
                getAdresProizvod(0);
                return;
            }

            btDelete.Enabled = true;
            btEdit.Enabled = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isActive"];
            getAdresProizvod((int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"]);
        }

        DataTable dtAdress;

        private void getAdresProizvod(int id_grp1)
        {
            Task<DataTable> task = Config.hCntMain.getGrp1VsGrp2(id_grp1);
            task.Wait();
            dtAdress = task.Result;
            setFilter_dop();
            dgvAdress.DataSource = dtAdress;
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                if (!(bool)dtData.DefaultView[e.RowIndex]["isActive"])
                    rColor = panel1.BackColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
        }

        private void get_data()
        {
            Task.Run(() =>
            {
                Config.DoOnUIThread(() => { this.Enabled = false; }, this);

                Task<DataTable> task = Config.hCntMain.getGrp1();
                task.Wait();
                dtData = task.Result;

                Config.DoOnUIThread(() =>
                {
                    DataGridViewColumn oldCol = dgvData.SortedColumn;
                    ListSortDirection direction = ListSortDirection.Ascending;
                    if (oldCol != null)
                    {
                        if (dgvData.SortOrder == System.Windows.Forms.SortOrder.Ascending)
                        {
                            direction = ListSortDirection.Ascending;
                        }
                        else
                        {
                            direction = ListSortDirection.Descending;
                        }
                    }
                    setFilter();
                    dgvData.DataSource = dtData;


                    if (oldCol != null)
                    {
                        dgvData.Sort(oldCol, direction);
                        oldCol.HeaderCell.SortGlyphDirection =
                            direction == ListSortDirection.Ascending ?
                            System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                    }

                }, this);

                Config.DoOnUIThread(() => { this.Enabled = true; }, this);
            });
        }

        private void setLog(int id, int id_log)
        {
            Logging.StartFirstLevel(id_log);
            switch (id_log)
            {
                case 2: Logging.Comment("Удаление Типа документа"); break;
                case 3: Logging.Comment("Тип документа переведён в недействующие "); break;
                case 4: Logging.Comment("Тип документа переведён  в действующие"); break;
                default: break;
            }

            string cName = (string)dtData.DefaultView[dgvData.CurrentRow.Index]["cName"];

            Logging.Comment($"ID:{id}");
            Logging.Comment($"Наименование: {cName}");

            Logging.StopFirstLevel();
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbName.Location = new Point(dgvData.Location.X, tbName.Location.Y);
            tbName.Size = new Size(cName.Width, tbName.Height);
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvAdress_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbNaneGrp.Location = new Point(dgvAdress.Location.X, tbNaneGrp.Location.Y);
            tbNaneGrp.Size = new Size(cNameGrp.Width, tbNaneGrp.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                new frmViewMngTuGrp() { row = row }.ShowDialog();
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
                if (col.Visible)
                {
                    maxColumns++;
                    if (col.Name.Equals("cName")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("cNds")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("cDeps")) setWidthColumn(indexRow, maxColumns, 22, report);                    
                    if (col.Name.Equals("cUL")) setWidthColumn(indexRow, maxColumns, 13, report);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Справочник ТУ групп", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отдел: {cmbDeps.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{label2.Text}: {cmbUL.Text}", indexRow, 1);
            indexRow++;
          
            if (tbName.Text.Trim().Length > 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтр: {tbName.Text}", indexRow, 1);
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
                if (col.Visible)
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            foreach (DataRowView row in dtData.DefaultView)
            {
                indexCol = 1;
                report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    if (col.Visible)
                    {
                        if (row[col.DataPropertyName] is DateTime)
                            report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                        else
                         if (row[col.DataPropertyName] is bool)
                            report.AddSingleValue((bool)row[col.DataPropertyName] ? "Да" : "Нет", indexRow, indexCol);
                        else
                           if (row[col.DataPropertyName] is decimal)
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
                if (!(bool)row["isActive"])
                    report.SetCellColor(indexRow, 1, indexRow, maxColumns, panel1.BackColor);

                indexRow++;
            }

            indexRow++;
            report.SetCellColor(indexRow, 1, indexRow, 1, panel1.BackColor);
            report.Merge(indexRow, 2, indexRow, maxColumns);
            report.AddSingleValue($"{chbNotActive.Text}", indexRow, 2);


            report.Show();
        }

        private void tbNaneGrp_TextChanged(object sender, EventArgs e)
        {
            setFilter_dop();
        }

        private void setFilter_dop()
        {
            if (dtAdress == null || dtAdress.Rows.Count == 0)
            {                
                return;
            }

            try
            {
                string filter = "";

                if (tbNaneGrp.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbNaneGrp.Text.Trim()}%'";

                dtAdress.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtAdress.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
              
            }
        }

        private void dgvAdress_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtAdress != null && dtAdress.DefaultView.Count != 0)
            {
                Color rColor = Color.White;

                dgvAdress.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvAdress.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvAdress.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

    }
}

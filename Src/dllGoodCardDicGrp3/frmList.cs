﻿using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllGoodCardDicGrp3
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
            

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btDelete, "Удалить");
            tp.SetToolTip(btClose, "Выход");
            btAdd.Visible = btEdit.Visible = btDelete.Visible = new List<string> { "РКВ" }.Contains(UserSettings.User.StatusCode);
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(true);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";
            cmbDeps.DataSource = dtObjectLease;

            get_data();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
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

                if (!chbNotActive.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                dtData.DefaultView.RowFilter = filter;
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
                return;
            }

            btDelete.Enabled = true;
            btEdit.Enabled = (bool)dtData.DefaultView[dgvData.CurrentRow.Index]["isActive"];
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

                Task<DataTable> task = Config.hCntMain.getGrp3();
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

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbName.Location = new Point(dgvData.Location.X, tbName.Location.Y);
            tbName.Size = new Size(cName.Width, tbName.Height);
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmAdd() { Text = "Добавить производителя" }.ShowDialog())
                get_data();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow != null && dgvData.CurrentRow.Index != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
                if (DialogResult.OK == new frmAdd() { Text = "Редактировать производителя", row = row }.ShowDialog())
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
                bool isActive = (bool)row["isActive"];
                bool isDel = true;
                int result = 0;
                bool isAutoIncriments = false;

                Task<DataTable> task = Config.hCntMain.setGrp3(id, cName, id_otdel, isActive, isDel, result, isAutoIncriments);
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
                        //setLog(id, 1542);
                        //task = Config.hCntMain.setProizvoditel(id, cName, code, id_type_org, !isActive, false, 0, false);
                        result = 1;
                        isDel = false;
                        task = Config.hCntMain.setGrp3(id, cName, id_otdel, !isActive, isDel, result, isAutoIncriments);
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
                        task = Config.hCntSecond.setGrp3(id, cName, id_otdel, !isActive, isDel, result, isAutoIncriments);
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
                       // setLog(id, 1566);
                        //task = Config.hCntMain.setProizvoditel(id, cName, code, id_type_org, isActive, true, 1, false);                        
                        //isAutoIncriments = true;
                        isDel = true;
                        result = 1;
                        task = Config.hCntMain.setGrp3(id, cName, id_otdel, isActive, isDel, result, isAutoIncriments);
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
                        task = Config.hCntSecond.setGrp3(id, cName, id_otdel,  isActive, isDel, result, isAutoIncriments);
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
                        //setLog(id, 1543);
                        //task = Config.hCntMain.setProizvoditel(id, cName, code, id_type_org, !isActive, false, 0, false);
                        //isAutoIncriments = true;
                        result = 1;
                        isDel = false;
                        task = Config.hCntMain.setGrp3(id, cName, id_otdel,  !isActive, isDel, result, isAutoIncriments);
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
                        task = Config.hCntSecond.setGrp3(id, cName, id_otdel, !isActive, isDel, result, isAutoIncriments);
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
            Close();
        }

        private void chbNotActive_CheckedChanged(object sender, EventArgs e)
        {
            setFilter();
        }
    }
}

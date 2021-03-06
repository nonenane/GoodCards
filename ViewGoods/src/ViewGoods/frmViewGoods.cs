﻿using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewGoods
{
    public partial class frmViewGoods : Form
    {
        private DataTable dtDeps,dtShop,dtGrp1, dtGrp2, dtGrp3,dtData;
        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;


        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dtGrp1 != null)
            {
                cmbGrp1.DataSource = dtGrp1.AsEnumerable()
                    .Where(r => r.Field<int>("id_otdel") == (int)cmbDeps.SelectedValue || r.Field<int>("id_otdel") == 0)
                    .OrderBy(r => r.Field<int>("isMain"))
                    .ThenBy(r=>r.Field<string>("cName") )
                    .CopyToDataTable();
                cmbGrp1.DisplayMember = "cName";
                cmbGrp1.ValueMember = "id";
            }


            if (dtGrp2 != null)
            {
                cmbGrp2.DataSource = dtGrp2.AsEnumerable()
                    .Where(r => r.Field<int>("id_otdel") == (int)cmbDeps.SelectedValue || r.Field<int>("id_otdel") == 0)
                    .OrderBy(r => r.Field<int>("isMain"))
                    .ThenBy(r => r.Field<string>("cName"))
                    .CopyToDataTable();
                cmbGrp2.DisplayMember = "cName";
                cmbGrp2.ValueMember = "id";
            }

            if (dtGrp3 != null)
            {
                cmbGrp3.DataSource = dtGrp3.AsEnumerable()
                    .Where(r => r.Field<int>("id_otdel") == (int)cmbDeps.SelectedValue || r.Field<int>("id_otdel") == 0)
                    .OrderBy(r => r.Field<int>("isMain"))
                    .ThenBy(r => r.Field<string>("cName"))
                    .CopyToDataTable();
                cmbGrp3.DisplayMember = "cName";
                cmbGrp3.ValueMember = "id";
            }
            setFilter();
        }

        private void cmbGrp1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbGrp2.SelectedIndex = 0;
            setFilter();
        }

        private void cmbGrp2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbGrp1.SelectedIndex = 0;
            setFilter();
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                if (new List<int>(new int[] { 1, 3 }).Contains((int)dtData.DefaultView[e.RowIndex]["ntypetovar"]))
                {
                    rColor = panel1.BackColor;
                }

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void tbEan_TextChanged(object sender, EventArgs e)
        {
            if (!chbNewGoods.Checked) return;
            setFilter();
        }

        private void tbEan_KeyDown(object sender, KeyEventArgs e)
        {
            if (chbNewGoods.Checked) return;
            if (e.KeyCode == Keys.Enter)
            {
                getData();                
            }
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

        public frmViewGoods()
        {
            InitializeComponent();

            if (Config.hCntMain==null)
            Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose,"Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btViewCartGoods, "Просмотр карточки товара");
            tp.SetToolTip(btClear, "Сброс фильтра");
        }

        private void chbNewGoods_Click(object sender, EventArgs e)
        {
            //btClear.Visible = !chbNewGoods.Checked;
            getData();
        }

        private void cmbGrp3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            setFilter();
        }

        private void chbReserv_Click(object sender, EventArgs e)
        {
            setFilter();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            tbEan.Text = "";
            tbName.Text = "";
            if (chbNewGoods.Checked)
            {
                setFilter();
                return;
            }
            getData();
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private async void btPrint_Click(object sender, EventArgs e)
        {
            report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;
            int maxColumns = 0;
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;
            var result = await Task<bool>.Factory.StartNew(() =>
            {

                foreach (DataGridViewColumn col in dgvData.Columns)
                    if (col.Visible)
                    {
                        maxColumns++;
                        if (col.Name.Equals(cDeps.Name)) setWidthColumn(indexRow, maxColumns, 13, report);
                        if (col.Name.Equals(cGrp1.Name)) setWidthColumn(indexRow, maxColumns, 13, report);
                        if (col.Name.Equals(cGrp2.Name)) setWidthColumn(indexRow, maxColumns, 14, report);
                        if (col.Name.Equals(cEan.Name)) setWidthColumn(indexRow, maxColumns, 15, report);
                        if (col.Name.Equals(cName.Name)) setWidthColumn(indexRow, maxColumns, 40, report);
                        if (col.Name.Equals(cPrice.Name)) setWidthColumn(indexRow, maxColumns, 11, report);
                        if (col.Name.Equals(cGrp3.Name)) setWidthColumn(indexRow, maxColumns, 11, report);

                    }


                #region "Head"
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"{this.Text}", indexRow, 1);
                report.SetFontBold(indexRow, 1, indexRow, 1);
                report.SetFontSize(indexRow, 1, indexRow, 1, 16);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
                indexRow++;
                indexRow++;

                Config.DoOnUIThread(() =>
                {
                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Магазин: {cmbShop.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Отдел: {cmbDeps.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Т/У группа: {cmbGrp1.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Инв. группа: {cmbGrp2.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Подгруппы: {cmbGrp3.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Новый товар: {(chbNewGoods.Checked ? "Да":"Нет")}", indexRow, 1);
                    indexRow++;

                    if (tbEan.Text.Trim().Length != 0 || tbName.Text.Trim().Length != 0)
                    {
                        report.Merge(indexRow, 1, indexRow, maxColumns);
                        report.AddSingleValue($"Фильтр: {(tbEan.Text.Trim().Length != 0 ? $"EAN:{tbEan.Text.Trim()} | " : "")} {(tbName.Text.Trim().Length != 0 ? $"Наименование:{tbName.Text.Trim()}" : "")}", indexRow, 1);
                        indexRow++;
                    }

                }, this);
                //report.Merge(indexRow, 1, indexRow, maxColumns);
                //report.AddSingleValue($"Должность: {cmbPost.Text}", indexRow, 1);
                //indexRow++;

                //report.Merge(indexRow, 1, indexRow, maxColumns);
                //report.AddSingleValue($"Место работы: {(rbOffice.Checked ? rbOffice.Text : rbUni.Text)}", indexRow, 1);
                //indexRow++;

                //report.Merge(indexRow, 1, indexRow, maxColumns);
                //report.AddSingleValue($"Статус сотрудника: {(rbWork.Checked ? rbWork.Text : rbUnemploy.Text)}", indexRow, 1);
                //indexRow++;

                //if (tbPostName.Text.Trim().Length != 0 || tbKadrName.Text.Trim().Length != 0)
                //{
                //    report.Merge(indexRow, 1, indexRow, maxColumns);
                //    report.AddSingleValue($"Фильтр: {(tbPostName.Text.Trim().Length != 0 ? $"Должность:{tbPostName.Text.Trim()} | " : "")} {(tbKadrName.Text.Trim().Length != 0 ? $"ФИО:{tbKadrName.Text.Trim()}" : "")}", indexRow, 1);
                //    indexRow++;
                //}

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
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                report.SetWrapText(indexRow, 1, indexRow, maxColumns);
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

                    if (chbReserv.Checked && new List<int>(new int[] { 1, 3 }).Contains((int)row["ntypetovar"]))
                        report.SetCellColor(indexRow, 1, indexRow, maxColumns, panel1.BackColor);

                    report.SetBorders(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);

                    indexRow++;
                }

                if (chbReserv.Checked)
                {
                    indexRow++;
                    report.SetCellColor(indexRow, 1, indexRow, 1, panel1.BackColor);
                    report.Merge(indexRow, 2, indexRow, maxColumns);
                    report.AddSingleValue($"{chbReserv.Text}", indexRow, 2);
                }

                Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);
                    progressBar1.Visible = false;
                }, this);

                report.Show();
                return true;
            });
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbShop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getData();
        }

        private void btViewCartGoods_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока не работает","oooops",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void frmViewGoods_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(true);
            task.Wait();

            dtDeps = task.Result;

            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";

            task = Config.hCntMain.getShop(false);
            task.Wait();

            dtShop = task.Result;

            cmbShop.DataSource = dtShop;
            cmbShop.DisplayMember = "cName";
            cmbShop.ValueMember = "id";

            task = Config.hCntMain.getGrp1(true);
            task.Wait();
            dtGrp1 = task.Result;

            task = Config.hCntMain.getGrp2(true);
            task.Wait();
            dtGrp2 = task.Result;

            task = Config.hCntMain.getGrp3(true);
            task.Wait();
            dtGrp3 = task.Result;

            cmbDeps_SelectionChangeCommitted(null, null);
            getData();
        }

        private void frmViewGoods_FormClosing(object sender, FormClosingEventArgs e)
        {

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
                    lFind.Location = new Point(tbEan.Location.X - 45, tbEan.Location.Y+4);
                }

                if (col.Name.Equals(cName.Name))
                {
                    tbName.Location = new Point(dgvData.Location.X + 1 + width, tbEan.Location.Y);
                    tbName.Size = new Size(cName.Width, tbEan.Size.Height);
                    btClear.Location = new Point(tbName.Location.X+tbName.Size.Width + 10, tbName.Location.Y-6);
                }

                width += col.Width;
            }
                
        }
 
        private async void getData()
        {            
            string ean = tbEan.Text.Trim().Length == 0 ? null : tbEan.Text.Trim().ToLower();
            string cName = tbName.Text.Trim().Length == 0 ? null : tbName.Text.Trim().ToLower();
            int id_Shop = (int)cmbShop.SelectedValue;
            bool isNewGoods = chbNewGoods.Checked;

            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;
            var result = await Task<bool>.Factory.StartNew(() =>
            {
                Task<DataTable> task;
                if (id_Shop == 1)
                    task = Config.hCntMain.getFindTovar(ean, cName, isNewGoods);
                else
                    task = Config.hCntSecond.getFindTovar(ean, cName, isNewGoods);
                task.Wait();
                dtData = task.Result;
                Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);
                    setFilter();
                    dgvData.DataSource = dtData;
                    progressBar1.Visible = false;
                }, this);

                return true;
            });          
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;
                btPrint.Enabled = btViewCartGoods.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbEan.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"ean like '%{tbEan.Text.Trim()}%'";

                if (tbName.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cname like '%{tbName.Text.Trim()}%'";

                if((int)cmbDeps.SelectedValue!=0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_otdel  = {cmbDeps.SelectedValue}";

                if ((int)cmbGrp1.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_grp1  = {cmbGrp1.SelectedValue}";

                if ((int)cmbGrp2.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_grp2  = {cmbGrp2.SelectedValue}";

                if ((int)cmbGrp3.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_grp3  = {cmbGrp3.SelectedValue}";

                if(!chbReserv.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"ntypetovar not in (1,3)";

                //if (!chbReserv.Checked)
                //  filter += (filter.Length == 0 ? "" : " and ") + $"isActive = 1";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btPrint.Enabled = btViewCartGoods.Enabled =
                dtData.DefaultView.Count != 0;
                //dgvData_SelectionChanged(null, null);
            }
        }
    }
}

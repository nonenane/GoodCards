using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewChangeGoods
{
    public partial class frmViewChangeGoods : Form
    {
        private DataTable dtDeps, dtShop, dtGrp1, dtGrp2, dtGrp3, dtData;

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dtGrp1 != null)
            {
                cmbGrp1.DataSource = dtGrp1.AsEnumerable()
                    .Where(r => r.Field<int>("id_otdel") == (int)cmbDeps.SelectedValue || r.Field<int>("id_otdel") == 0)
                    .OrderBy(r => r.Field<int>("isMain"))
                    .ThenBy(r => r.Field<string>("cName"))
                    .CopyToDataTable();
                cmbGrp1.DisplayMember = "cName";
                cmbGrp1.ValueMember = "id";
            }

            setFilter();
        }

        private void cmbGrp1_SelectionChangeCommitted(object sender, EventArgs e)
        {
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
            setFilter();
        }

        private void tbEan_KeyDown(object sender, KeyEventArgs e)
        {
            setFilter();
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

        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0)
            {
                tbDate.Text = tbFIO.Text = "";
                return;
            }


            tbDate.Text = ((DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["timeAfter"]).ToString();
            tbFIO.Text = dtData.DefaultView[dgvData.CurrentRow.Index]["FIO"].ToString();
            
        }

        private async void getData()
        {
            int id_Shop = (int)cmbShop.SelectedValue;
            DateTime date = dtpDate.Value.Date;
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;
            var result = await Task<bool>.Factory.StartNew(() =>
            {
                Task<DataTable> task;
                if (id_Shop == 1)
                    task = Config.hCntMainKassRealizz.GetChangeGoods(date);
                else
                    task = Config.hCntSecondKassRealizz.GetChangeGoods(date);
                task.Wait();
                dtData = task.Result;

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    task = Config.hCntMain.getGrp1(false, false);
                    task.Wait();
                    DataTable _dtGrp1 = task.Result;

                    foreach (DataRow row in dtData.Rows)
                    {
                        EnumerableRowCollection<DataRow> rowCollect = dtDeps.AsEnumerable()
                            .Where(r => r.Field<int>("id") == (int)row["id_departments"]);
                        if (rowCollect.Count() > 0) row["nameDep"] = rowCollect.First()["cName"];

                        rowCollect = _dtGrp1.AsEnumerable()
                                .Where(r => r.Field<int>("id") == (Int16)row["grpAtfer"]);
                        if (rowCollect.Count() > 0) row["nameGrpAtfer"] = rowCollect.First()["cName"];

                        rowCollect = _dtGrp1.AsEnumerable()
                            .Where(r => r.Field<int>("id") == (Int16)row["grpBefore"]);
                        if (rowCollect.Count() > 0) row["nameGrpBefore"] = rowCollect.First()["cName"];
                    }
                }

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

                if ((int)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_departments  = {cmbDeps.SelectedValue}";

                if ((int)cmbGrp1.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"grpAtfer  = {cmbGrp1.SelectedValue}";

                //if (!chbReserv.Checked)
                //    filter += (filter.Length == 0 ? "" : " and ") + $"ntypetovar not in (1,3)";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id_departments = -1";
            }
            finally
            {
                btPrint.Enabled = btViewCartGoods.Enabled =
                dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        public frmViewChangeGoods()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btPrint, "Печать");
            tp.SetToolTip(btViewCartGoods, "Просмотр карточки товара");
            dgvData_ColumnWidthChanged(null, null);
        }

        private void frmViewChangeGoods_Load(object sender, EventArgs e)
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

            cmbDeps_SelectionChangeCommitted(null, null);
            getData();
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

                width += col.Width;
            }

        }

    }
}

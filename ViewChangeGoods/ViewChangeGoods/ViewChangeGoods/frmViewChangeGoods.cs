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
        private DataTable dtDeps, dtShop, dtGrp1, dtData, dtDataPrice, dtDataNewGoods;
        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;

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

            filterTab();
        }

        private void cmbGrp1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterTab();
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView _grd = (sender as DataGridView);
            DataTable dt = (_grd.DataSource as DataTable);

            if (e.RowIndex != -1 && dt != null && dt.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                //if (new List<int>(new int[] { 1, 3 }).Contains((int)dtData.DefaultView[e.RowIndex]["ntypetovar"]))
                if((bool)dt.DefaultView[e.RowIndex]["isReserv"])
                {
                    rColor = panel1.BackColor;
                }

                _grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                _grd.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                _grd.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
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

       
        private void btUpdate_Click(object sender, EventArgs e)
        {
            getCombi();
            getData();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbEan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
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


                task = Config.hCntMain.getGrp1(false, false);
                task.Wait();
                DataTable _dtGrp1 = task.Result;

                #region "Изменение товара"
                if (id_Shop == 1)
                    task = Config.hCntMainKassRealizz.GetChangeGoods(date);
                else
                    task = Config.hCntSecondKassRealizz.GetChangeGoods(date);
                task.Wait();
                dtData = task.Result;

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    DataTable dtTmp = dtData.Clone();

                    var query = from g in dtData.AsEnumerable()
                                join k in dtDeps.AsEnumerable() on g.Field<int>("id_departments") equals k.Field<int>("id")
                                join g1 in _dtGrp1.AsEnumerable() on g.Field<Int16>("grpAtfer") equals g1.Field<int>("id")
                                join g2 in _dtGrp1.AsEnumerable() on g.Field<Int16>("grpBefore") equals g2.Field<int>("id")
                                select dtTmp.LoadDataRow(new object[]
                                                               {
                                                                    g.Field<int>("id_departments"),
                                                                    k.Field<string>("cName"),
                                                                    g.Field<string>("ean"),
                                                                    g.Field<string>("nameAtfer"),
                                                                    g.Field<string>("nameBefore"),
                                                                    g.Field<Int16>("grpAtfer"),
                                                                    g.Field<Int16>("grpBefore"),
                                                                    g1.Field<string>("cName"),
                                                                    g2.Field<string>("cName"),
                                                                    g.Field<DateTime>("timeAfter"),
                                                                    g.Field<DateTime>("timeBefore"),
                                                                    g.Field<Int16>("dptAtfer"),
                                                                    g.Field<Int16>("dptBefore"),
                                                                    g.Field<Int16>("taxAtfer"),
                                                                    g.Field<Int16>("taxBefore"),
                                                                    g.Field<string>("ulAfter"),
                                                                    g.Field<string>("ulBefore"),
                                                                    g.Field<string>("sender"),
                                                                    g.Field<string>("FIO"),
                                                                    g.Field<string>("nameAtfer").ToLower().Contains("резерв")

                                                               }, false);


                    dtData = query
                    .CopyToDataTable();



                    ////foreach (DataRow row in dtData.Rows)
                    ////{
                    ////    EnumerableRowCollection<DataRow> rowCollect = dtDeps.AsEnumerable()
                    ////        .Where(r => r.Field<int>("id") == (int)row["id_departments"]);
                    ////    if (rowCollect.Count() > 0) row["nameDep"] = rowCollect.First()["cName"];
                    ////    if ((int)row["id_departments"] == 6) row["nameDep"] = "ВВО";

                    ////    rowCollect = _dtGrp1.AsEnumerable()
                    ////            .Where(r => r.Field<int>("id") == (Int16)row["grpAtfer"]);
                    ////    if (rowCollect.Count() > 0) row["nameGrpAtfer"] = rowCollect.First()["cName"];

                    ////    rowCollect = _dtGrp1.AsEnumerable()
                    ////        .Where(r => r.Field<int>("id") == (Int16)row["grpBefore"]);
                    ////    if (rowCollect.Count() > 0) row["nameGrpBefore"] = rowCollect.First()["cName"];

                    ////    row["isReserv"] = ((string)row["nameAtfer"]).ToLower().Contains("резерв");
                    ////}
                }

                #endregion

                #region "Изменение цены"
                if (id_Shop == 1)
                    task = Config.hCntMainKassRealizz.GetChangeGoodsPrice(date);
                else
                    task = Config.hCntSecondKassRealizz.GetChangeGoodsPrice(date);
                task.Wait();
                dtDataPrice = task.Result;

                if (dtDataPrice != null && dtDataPrice.Rows.Count > 0)
                {
                    var groupTovar = dtDataPrice.AsEnumerable().GroupBy(g => new { ean = g.Field<string>("ean").Trim() }).Select(s => new { s.Key.ean });

                    string listEan = "";

                    foreach (var gEan in groupTovar)
                    {
                        listEan += (listEan.Trim().Length == 0 ? "" : ",") + gEan.ean;
                    }


                    if (id_Shop == 1)
                        task = Config.hCntMain.GetOstForMorning(date, listEan);
                    else
                        task = Config.hCntSecond.GetOstForMorning(date, listEan);
                    task.Wait();
                    DataTable dtOst = task.Result;



                    DataTable dtTmp = dtDataPrice.Clone();

                    var query = from g in dtDataPrice.AsEnumerable()
                                join dep in dtDeps.AsEnumerable() on g.Field<int>("id_departments") equals dep.Field<int>("id")
                                join g1 in _dtGrp1.AsEnumerable() on g.Field<Int16>("grpAtfer") equals g1.Field<int>("id")
                                join g2 in _dtGrp1.AsEnumerable() on g.Field<Int16>("grpBefore") equals g2.Field<int>("id")
                                join k in dtOst.AsEnumerable() on new { Q = g.Field<string>("ean").Trim() } equals new { Q = k.Field<string>("ean").Trim() } into t1
                                from leftjoin1 in t1.DefaultIfEmpty()
                                select dtTmp.LoadDataRow(new object[]
                                                               {
                                                                    g.Field<int>("id_departments"),
                                                                    dep.Field<string>("cName"),
                                                                    g.Field<string>("ean"),
                                                                    g.Field<string>("nameAtfer"),
                                                                    g.Field<string>("nameBefore"),
                                                                    g.Field<Int16>("grpAtfer"),
                                                                    g.Field<Int16>("grpBefore"),
                                                                    g1.Field<string>("cName"),
                                                                    g2.Field<string>("cName"),
                                                                    g.Field<DateTime>("timeAfter"),
                                                                    g.Field<DateTime>("timeBefore"),
                                                                    g.Field<Int16>("dptAtfer"),
                                                                    g.Field<Int16>("dptBefore"),
                                                                    g.Field<Int16>("taxAtfer"),
                                                                    g.Field<Int16>("taxBefore"),
                                                                    g.Field<string>("ulAfter"),
                                                                    g.Field<string>("ulBefore"),
                                                                    g.Field<string>("sender"),
                                                                    g.Field<string>("FIO"),
                                                                    g.Field<string>("nameAtfer").ToLower().Contains("резерв"),
                                                                    g.Field<decimal>("priceAfter"),
                                                                    g.Field<decimal>("priceBefore"),
                                                                    g.Field<decimal>("countForSell"),
                                                                    //g.Field<decimal>("ostForMorning"),
                                                                    leftjoin1 == null ? null : leftjoin1.Field<decimal?>("netto"),
                                                               }, false);


                    dtDataPrice = query
                    .CopyToDataTable();




                    //foreach (DataRow row in dtDataPrice.Rows)
                    //{
                    //    EnumerableRowCollection<DataRow> rowCollect = dtDeps.AsEnumerable()
                    //        .Where(r => r.Field<int>("id") == (int)row["id_departments"]);
                    //    if (rowCollect.Count() > 0) row["nameDep"] = rowCollect.First()["cName"];
                    //    if ((int)row["id_departments"] == 6) row["nameDep"] = "ВВО";

                    //    rowCollect = _dtGrp1.AsEnumerable()
                    //            .Where(r => r.Field<int>("id") == (Int16)row["grpAtfer"]);
                    //    if (rowCollect.Count() > 0) row["nameGrpAtfer"] = rowCollect.First()["cName"];

                    //    rowCollect = _dtGrp1.AsEnumerable()
                    //        .Where(r => r.Field<int>("id") == (Int16)row["grpBefore"]);
                    //    if (rowCollect.Count() > 0) row["nameGrpBefore"] = rowCollect.First()["cName"];

                    //    row["isReserv"] = ((string)row["nameAtfer"]).ToLower().Contains("резерв");

                    //    if (dtOst != null && dtOst.Rows.Count > 0)
                    //    {
                    //        rowCollect = dtOst.AsEnumerable()
                    //                .Where(r => r.Field<string>("ean") == (string)row["ean"]);
                    //        if (rowCollect.Count() > 0) row["ostForMorning"] = rowCollect.First()["netto"];
                    //    }
                    //}
                }

                #endregion

                #region "Новые товары"
                if (id_Shop == 1)
                    task = Config.hCntMainKassRealizz.GetNewGoods(date);
                else
                    task = Config.hCntSecondKassRealizz.GetNewGoods(date);
                task.Wait();
                dtDataNewGoods = task.Result;

                if (dtDataNewGoods != null && dtDataNewGoods.Rows.Count > 0)
                {

                    DataTable dtTmp = dtDataNewGoods.Clone();

                    var query = from g in dtDataNewGoods.AsEnumerable()
                                join k in dtDeps.AsEnumerable() on g.Field<int>("id_departments") equals k.Field<int>("id")
                                join g1 in _dtGrp1.AsEnumerable() on g.Field<Int16>("grpAtfer") equals g1.Field<int>("id")                                
                                select dtTmp.LoadDataRow(new object[]
                                                               {
                                                                    g.Field<int>("id_departments"),
                                                                    k.Field<string>("cName"),
                                                                    g.Field<string>("ean"),
                                                                    g.Field<string>("nameAtfer"),                                                                    
                                                                    g.Field<Int16>("grpAtfer"),                                                                    
                                                                    g1.Field<string>("cName"),                                                                    
                                                                    g.Field<DateTime>("timeAfter"),                                                                    
                                                                    g.Field<Int16>("dptAtfer"),                                                                    
                                                                    g.Field<Int16>("taxAtfer"),                                                                    
                                                                    g.Field<string>("ulAfter"),                                                                    
                                                                    g.Field<string>("sender"),
                                                                    g.Field<decimal>("priceAfter"),
                                                                    g.Field<string>("FIO"),
                                                                    g.Field<string>("nameAtfer").ToLower().Contains("резерв")

                                                               }, false);


                    dtDataNewGoods = query
                    .CopyToDataTable();





                    //foreach (DataRow row in dtDataNewGoods.Rows)
                    //{
                    //    EnumerableRowCollection<DataRow> rowCollect = dtDeps.AsEnumerable()
                    //        .Where(r => r.Field<int>("id") == (int)row["id_departments"]);
                    //    if (rowCollect.Count() > 0) row["nameDep"] = rowCollect.First()["cName"];
                    //    if ((int)row["id_departments"] == 6) row["nameDep"] = "ВВО";                                         

                    //    row["isReserv"] = ((string)row["nameAtfer"]).ToLower().Contains("резерв");
                    //}
                }

                #endregion

                Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);                    
                    dgvData.DataSource = dtData;
                    dgvDataPrice.DataSource = dtDataPrice;
                    dgvDataNewGoods.DataSource = dtDataNewGoods;
                    filterTab();
                    progressBar1.Visible = false;
                }, this);

                return true;
            });
        }

        private void chbReserv_CheckedChanged(object sender, EventArgs e)
        {
            filterTab();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterTab();
        }

        private void filterTab()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: 
                    setFilter();break;
                case 1:
                    setFilterPrice(); break;
                case 2: 
                    setFilterNewGoods(); break;
            }
        }

        private async void btViewCartGoods_Click(object sender, EventArgs e)
        {
            report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            TabPage tp = tabControl1.SelectedTab;
            DataGridView grd = null;
            DataTable dt = null;

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    grd = dgvData;
                    dt = dgvData.DataSource as DataTable; 
                    break;
                case 1:
                    grd = dgvDataPrice;
                    dt = dgvDataPrice.DataSource as DataTable; 
                    break;
                case 2:
                    grd = dgvDataNewGoods;
                    dt = dgvDataNewGoods.DataSource as DataTable;
                    break;
            }


            int indexRow = 1;
            int maxColumns = 0;
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;
            var result = await Task<bool>.Factory.StartNew(() =>
            {

                foreach (DataGridViewColumn col in grd.Columns)
                    if (col.Visible)
                    {

                        maxColumns++;
                        if (new List<string>(new string[] { cDeps.Name, cNameDepPrice.Name, cNameDepNewGoods.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 13, report);
                        if (new List<string>(new string[] { cGrp1.Name, nameGrp1Atfer.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 13, report);
                        if (new List<string>(new string[] { cEan.Name, cEanPrice.Name, cEanNewGoods.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 15, report);
                        if (new List<string>(new string[] { cNameBefore.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 40, report);
                        if (new List<string>(new string[] { cNameAfter.Name, cNamePrice.Name, cNameNewGoods.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 40, report);
                        if (new List<string>(new string[] { cPriceBefore.Name, cPriceNewGoods.Name, cPriceAfter.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 11, report);
                        if (new List<string>(new string[] { cSell.Name, cOstMorning.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 11, report);
                        if (new List<string>(new string[] { cNdsBefore.Name, cNdsAfter.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 11, report);
                        if (new List<string>(new string[] { cUlBefore.Name, cUlAfter.Name, cUlNewGoods.Name }).Contains(col.Name)) setWidthColumn(indexRow, maxColumns, 11, report);
                    }


                #region "Head"
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"{tp.Text}", indexRow, 1);
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
                    report.AddSingleValue($"{label4.Text}: {dtpDate.Value.ToShortDateString()}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Отдел: {cmbDeps.Text}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, maxColumns);
                    report.AddSingleValue($"Т/У группа: {cmbGrp1.Text}", indexRow, 1);
                    indexRow++;


                    //if (tbEan.Text.Trim().Length != 0 || tbName.Text.Trim().Length != 0)
                    //{
                    //    report.Merge(indexRow, 1, indexRow, maxColumns);
                    //    report.AddSingleValue($"Фильтр: {(tbEan.Text.Trim().Length != 0 ? $"EAN:{tbEan.Text.Trim()} | " : "")} {(tbName.Text.Trim().Length != 0 ? $"Наименование:{tbName.Text.Trim()}" : "")}", indexRow, 1);
                    //    indexRow++;
                    //}

                }, this);

                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
                indexRow++;
                indexRow++;
                #endregion

                int indexCol = 0;
                foreach (DataGridViewColumn col in grd.Columns)
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

                foreach (DataRowView row in dt.DefaultView)
                {
                    indexCol = 1;
                    report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                    foreach (DataGridViewColumn col in grd.Columns)
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

                    if (chbReserv.Checked && (bool)row["isReserv"])
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

                report.SetPageSetup(1, 9999, true);
                report.Show();
                return true;
            });
        }


        public frmViewChangeGoods()
        {
            InitializeComponent();

            if (Config.hCntMain == null)
                Config.hCntMain = new Procedures(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Config.hCntSecond == null)
                Config.hCntSecond = new Procedures(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            dgvData.AutoGenerateColumns = false;
            dgvDataPrice.AutoGenerateColumns = false;
            dgvDataNewGoods.AutoGenerateColumns = false;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btPrint, "Сравнение цены товара на кассе и в ПО \"ТК\"");
            tp.SetToolTip(btViewCartGoods, "Печать");
            dgvData_ColumnWidthChanged(null, null);
            dgvDataPrice_ColumnWidthChanged(null, null);
            dgvDataNewGoods_ColumnWidthChanged(null, null);
        }

        private void frmViewChangeGoods_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getShop(false);
            task.Wait();

            dtShop = task.Result;

            cmbShop.DataSource = dtShop;
            cmbShop.DisplayMember = "cName";
            cmbShop.ValueMember = "id";

            getCombi();
            getData();
        }

        private void getCombi()
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(true);
            task.Wait();

            dtDeps = task.Result;

            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";

            task = Config.hCntMain.getGrp1(true);
            task.Wait();
            dtGrp1 = task.Result;

            cmbDeps_SelectionChangeCommitted(null, null);
        }

        #region "Изменение товара"

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

        private void tbEan_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0)
            {
                tbDate.Text = tbFIO.Text = "";
                return;
            }

            try
            {
                tbDate.Text = ((DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["timeAfter"]).ToString();
                tbFIO.Text = dtData.DefaultView[dgvData.CurrentRow.Index]["FIO"].ToString();
            }
            catch {
                tbDate.Text = tbFIO.Text = "";
                return;
            }

        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;
                //btPrint.Enabled = 
                tbFIO.Text = tbDate.Text = "";
                btViewCartGoods.Enabled = false;
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

                if (!chbReserv.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isReserv  = 0";


                dtData.DefaultView.RowFilter = filter;
                dtData.DefaultView.Sort = "ean asc, timeBefore asc";
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id_departments = -1";
            }
            finally
            {
                //btPrint.Enabled = 
                    btViewCartGoods.Enabled =
                dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        #endregion

        #region "Изменение цены"

        private void tbEanPrice_TextChanged(object sender, EventArgs e)
        {
            setFilterPrice();
        }

        private void dgvDataPrice_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDataPrice.CurrentRow == null || dgvDataPrice.CurrentRow.Index == -1 || dtDataPrice == null || dtDataPrice.DefaultView.Count == 0)
            {
                tbDate.Text = tbFIO.Text = "";
                return;
            }

            try { 
            tbDate.Text = ((DateTime)dtDataPrice.DefaultView[dgvDataPrice.CurrentRow.Index]["timeAfter"]).ToString();
            tbFIO.Text = dtDataPrice.DefaultView[dgvDataPrice.CurrentRow.Index]["FIO"].ToString();
            }
            catch
            {
                tbDate.Text = tbFIO.Text = "";
                return;
            }
        }
       
        private void dgvDataPrice_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvDataPrice.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cEanPrice.Name))
                {
                    tbEanPrice.Location = new Point(dgvDataPrice.Location.X + 1 + width, tbEanPrice.Location.Y);
                    tbEanPrice.Size = new Size(cEanPrice.Width, tbEanPrice.Size.Height);
                }

                if (col.Name.Equals(cNamePrice.Name))
                {
                    tbNamePrice.Location = new Point(dgvDataPrice.Location.X + 1 + width, tbEanPrice.Location.Y);
                    tbNamePrice.Size = new Size(cNamePrice.Width, tbNamePrice.Size.Height);
                }

                width += col.Width;
            }
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            new frmReport() { Text = "Сравнение цены товара на кассе и в ПО \"ТК\"" }.ShowDialog();
        }

        private void tbEan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                string text = Clipboard.GetText();
                decimal otDec;
                if (decimal.TryParse(text, out otDec))
                {
                    TextBox tb = sender as TextBox;
                    tb.Text = text;
                }
            }
        }

        private void setFilterPrice()
        {
            if (dtDataPrice == null || dtDataPrice.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;
                tbFIO.Text = tbDate.Text = "";
                btPrint.Enabled = btViewCartGoods.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbEanPrice.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"ean like '%{tbEanPrice.Text.Trim()}%'";

                if (tbNamePrice.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameAtfer like '%{tbNamePrice.Text.Trim()}%'";

                if ((int)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_departments  = {cmbDeps.SelectedValue}";

                if ((int)cmbGrp1.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"grpAtfer  = {cmbGrp1.SelectedValue}";

                if (!chbReserv.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isReserv  = 0";

                dtDataPrice.DefaultView.RowFilter = filter;
                dtDataPrice.DefaultView.Sort = "ean asc, timeBefore asc";
            }
            catch
            {
                dtDataPrice.DefaultView.RowFilter = "id_departments = -1";
            }
            finally
            {
                btPrint.Enabled = btViewCartGoods.Enabled =
                dtDataPrice.DefaultView.Count != 0;
                dgvDataPrice_SelectionChanged(null, null);
            }
        }

        #endregion

        #region "Новые товары"

        private void dgvDataNewGoods_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDataNewGoods.CurrentRow == null || dgvDataNewGoods.CurrentRow.Index == -1 || dtDataNewGoods == null || dtDataNewGoods.DefaultView.Count == 0)
            {
                tbDate.Text = tbFIO.Text = "";
                return;
            }

            try { 
            tbDate.Text = ((DateTime)dtDataNewGoods.DefaultView[dgvDataNewGoods.CurrentRow.Index]["timeAfter"]).ToString();
            tbFIO.Text = dtDataNewGoods.DefaultView[dgvDataNewGoods.CurrentRow.Index]["FIO"].ToString();
            }
            catch
            {
                tbDate.Text = tbFIO.Text = "";
                return;
            }
        }

        private void dgvDataNewGoods_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvDataNewGoods.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cEanNewGoods.Name))
                {
                    tbEanNewGoods.Location = new Point(dgvDataNewGoods.Location.X + 1 + width, tbEanNewGoods.Location.Y);
                    tbEanNewGoods.Size = new Size(cEanNewGoods.Width, tbEanNewGoods.Size.Height);
                }

                if (col.Name.Equals(cNameNewGoods.Name))
                {
                    tbNameNewGoods.Location = new Point(dgvDataNewGoods.Location.X + 1 + width, tbEanNewGoods.Location.Y);
                    tbNameNewGoods.Size = new Size(cNameNewGoods.Width, tbEanNewGoods.Size.Height);
                }

                width += col.Width;
            }
        }

        private void setFilterNewGoods()
        {
            if (dtDataNewGoods == null || dtDataNewGoods.Rows.Count == 0)
            {
                //btEdit.Enabled = btDelete.Enabled = false;
                tbFIO.Text = tbDate.Text = "";
                btPrint.Enabled = btViewCartGoods.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbEanNewGoods.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"ean like '%{tbEanNewGoods.Text.Trim()}%'";

                if (tbNameNewGoods.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"nameAtfer like '%{tbNameNewGoods.Text.Trim()}%'";

                if ((int)cmbDeps.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"id_departments  = {cmbDeps.SelectedValue}";

                if ((int)cmbGrp1.SelectedValue != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"grpAtfer  = {cmbGrp1.SelectedValue}";

                if (!chbReserv.Checked)
                    filter += (filter.Length == 0 ? "" : " and ") + $"isReserv  = 0";

                dtDataNewGoods.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtDataNewGoods.DefaultView.RowFilter = "id_departments = -1";
            }
            finally
            {
                btPrint.Enabled = btViewCartGoods.Enabled =
                dtDataNewGoods.DefaultView.Count != 0;
                dgvDataNewGoods_SelectionChanged(null, null);
            }
        }

        private void tbEanNewGoods_TextChanged(object sender, EventArgs e)
        {
            setFilterNewGoods();
        }

        #endregion
    }
}

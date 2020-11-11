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

namespace ViewChangeGoods
{
    public partial class frmReport : Form
    {
        private Nwuram.Framework.UI.Service.EnableControlsServiceInProg blockers = new Nwuram.Framework.UI.Service.EnableControlsServiceInProg();
        private Nwuram.Framework.ToExcelNew.ExcelUnLoad report = null;

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(true);
            task.Wait();

           DataTable dtDeps = task.Result;

            cmbDeps.DataSource = dtDeps;
            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";

            task = Config.hCntMain.getShop(false);
            task.Wait();

            DataTable dtShop = task.Result;

            cmbShop.DataSource = dtShop;
            cmbShop.DisplayMember = "cName";
            cmbShop.ValueMember = "id";
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btPrint_Click(object sender, EventArgs e)
        {
            int id_Shop = (int)cmbShop.SelectedValue;
            int id_dep = (int)cmbDeps.SelectedValue;

            report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
            blockers.SaveControlsEnabledState(this);
            blockers.SetControlsEnabled(this, false);
            progressBar1.Visible = true;

            var result = await Task<bool>.Factory.StartNew(() =>
            {
                DataTable dtDbase1, dtKassRealiz;
                Task<DataTable> task;

                if (id_Shop == 1)
                {
                    task = Config.hCntMain.GetListTovarForValidateReport(id_dep);
                    task.Wait();
                    dtDbase1 = task.Result;

                    task = Config.hCntMainKassRealizz.GetListTovarForValidateReport(id_dep);
                    task.Wait();
                    dtKassRealiz = task.Result;                    
                }
                else
                {
                    task = Config.hCntSecond.GetListTovarForValidateReport(id_dep);
                    task.Wait();
                    dtDbase1 = task.Result;

                    task = Config.hCntSecondKassRealizz.GetListTovarForValidateReport(id_dep);
                    task.Wait();
                    dtKassRealiz = task.Result;
                }


                if (dtKassRealiz != null && dtKassRealiz.Rows.Count > 0)
                {
                    foreach (DataRow row in dtDbase1.Rows)
                    {
                        EnumerableRowCollection<DataRow> rowCollect = dtKassRealiz.AsEnumerable()
                        .Where(r => r.Field<string>("ean") == (string)row["ean"] && Math.Abs(((DateTime)row["DateCreate"] - r.Field<DateTime>("s_time")).TotalSeconds) < 4);

                        if (rowCollect.Count() > 0)
                        {
                            row["priceKass"] = rowCollect.First()["price"];
                            row["FIOKass"] = rowCollect.First()["FIO"];
                            row["dateKass"] = rowCollect.First()["s_time"];

                            rowCollect.First().Delete();
                        }
                    }                
                }


                if (dtDbase1 == null || dtDbase1.Rows.Count == 0)
                {
                    Config.DoOnUIThread(() =>
                    {
                        MessageBox.Show("Нет данных для выгрузки", "Выгрузка отчёта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        blockers.RestoreControlEnabledState(this);
                        progressBar1.Visible = false;
                    }, this);
                    return false;
                }


                int indexRow = 1;
                int maxColumns = 9;

                setWidthColumn(indexRow, 1, 8, report);
                setWidthColumn(indexRow, 2, 12, report);
                setWidthColumn(indexRow, 3, 30, report);
                setWidthColumn(indexRow, 4, 10, report);
                setWidthColumn(indexRow, 5, 17, report);
                setWidthColumn(indexRow, 6, 18, report);
                setWidthColumn(indexRow, 7, 10, report);
                setWidthColumn(indexRow, 8, 17, report);
                setWidthColumn(indexRow, 9, 18, report);



                #region "Head"
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"{this.Text}", indexRow, 1);
                report.SetFontBold(indexRow, 1, indexRow, 1);
                report.SetFontSize(indexRow, 1, indexRow, 1, 16);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
                indexRow++;
                indexRow++;


                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
                indexRow++;
                indexRow++;
                #endregion

                report.Merge(indexRow, 1, indexRow + 1, 1);
                report.AddSingleValue("№", indexRow, 1);

                report.Merge(indexRow, 2, indexRow + 1, 2);
                report.AddSingleValue("Отдел", indexRow, 2);

                report.Merge(indexRow, 3, indexRow + 1, 3);
                report.AddSingleValue("Наименование", indexRow, 3);


                report.Merge(indexRow, 4, indexRow , 6);
                report.AddSingleValue("На кассе", indexRow, 4);
                report.AddSingleValue("Цена", indexRow+1, 4);
                report.AddSingleValue("Дата установки", indexRow + 1, 5);
                report.AddSingleValue("Выгрузил на кассу", indexRow + 1, 6);


                report.Merge(indexRow, 7, indexRow, 9);
                report.AddSingleValue("В программе ТК", indexRow, 7);
                report.AddSingleValue("Цена", indexRow + 1, 7);
                report.AddSingleValue("Дата установки", indexRow + 1, 8);
                report.AddSingleValue("Сохранил в программе", indexRow + 1, 9);
                report.SetFontBold(indexRow, 1, indexRow+1, maxColumns);
                report.SetBorders(indexRow, 1, indexRow+1, maxColumns);
                report.SetWrapText(indexRow, 1, indexRow+1, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow+1, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow+1, maxColumns);
                indexRow++;
                indexRow++;

                int npp = 1;
                foreach (DataRow row in dtDbase1.Rows)
                {
                    report.SetWrapText(indexRow, 1, indexRow, maxColumns);

                    addDataToCell(npp, indexRow, 1, report);
                    addDataToCell(row["nameDep"], indexRow, 2, report);
                    addDataToCell(row["cname"], indexRow, 3, report);
                    
                    addDataToCell(row["priceKass"], indexRow, 4, report);
                    addDataToCell(row["dateKass"], indexRow, 5, report);
                    addDataToCell(row["FIOKass"], indexRow, 6, report);
                    

                    addDataToCell(row["rcena"], indexRow, 7, report);
                    addDataToCell(row["DateCreate"], indexRow, 8, report);
                    addDataToCell(row["FIO"], indexRow, 9, report);


                    report.SetBorders(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                    report.SetCellAlignmentToRight(indexRow, 4, indexRow, 4);
                    indexRow++;
                    npp++;
                }

                report.SetPageSetup(1, 9999, false);
                report.Show();


                Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);                  
                    progressBar1.Visible = false;
                }, this);

                return true;
            });

        }


        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void addDataToCell(object value, int indexRow, int indexCol, Nwuram.Framework.ToExcelNew.ExcelUnLoad report, bool isFullTime = false)
        {
            if (value is DateTime)
            {
                if (isFullTime)
                    report.AddSingleValue(((DateTime)value).ToString(), indexRow, indexCol);
                else
                    report.AddSingleValue(((DateTime)value).ToShortDateString(), indexRow, indexCol);
            }
            else if (value is bool)
                report.AddSingleValue((bool)value ? "Да" : "Нет", indexRow, indexCol);
            else if (value is decimal || value is double)
            {
                report.AddSingleValueObject(value, indexRow, indexCol);
                report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
            }
            else
                report.AddSingleValue(value.ToString(), indexRow, indexCol);
        }

    }
}

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
                        .Where(r => r.Field<string>("ean") == (string)row["ean"]);

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


                    Config.DoOnUIThread(() =>
                {
                    blockers.RestoreControlEnabledState(this);                  
                    progressBar1.Visible = false;
                }, this);

                return true;
            });

        }
    }
}

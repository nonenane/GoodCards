using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllGoodCardDicTuGrp
{
    public partial class frmViewMngTuGrp : Form
    {
        public DataRowView row { set; private get; }
        private DataTable dtData;
        public frmViewMngTuGrp()
        {
            InitializeComponent();
            dgvAdress.AutoGenerateColumns = false;
        }

        private void dgvAdress_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbNaneGrp.Location = new Point(dgvAdress.Location.X, tbNaneGrp.Location.Y);
            tbNaneGrp.Size = new Size(cNameGrp.Width, tbNaneGrp.Height);
        }

        private void frmViewMngTuGrp_Load(object sender, EventArgs e)
        {
            tbNameGrp.Text = (string)row["cName"];
            Task<DataTable> task = Config.hCntMain.getUserVsGrp1((int)row["id"]);
            task.Wait();
            dtData = task.Result;
            dgvAdress.DataSource = dtData;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btPrint.Enabled = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbNaneGrp.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"FIO like '%{tbNaneGrp.Text.Trim()}%'";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btPrint.Enabled =
                dtData.DefaultView.Count != 0;                
            }
        }

        private void tbNaneGrp_TextChanged(object sender, EventArgs e)
        {
            setFilter();
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

            foreach (DataGridViewColumn col in dgvAdress.Columns)
                if (col.Visible)
                {
                    maxColumns++;
                    if (col.Name.Equals("cNameGrp")) setWidthColumn(indexRow, maxColumns, 60, report);
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
            report.AddSingleValue($"{tbNameGrp.Text}: {tbNameGrp.Text}", indexRow, 1);
            indexRow++;

            if (tbNaneGrp.Text.Trim().Length > 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтр: {tbNaneGrp.Text}", indexRow, 1);
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
            foreach (DataGridViewColumn col in dgvAdress.Columns)
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
                foreach (DataGridViewColumn col in dgvAdress.Columns)
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

                indexRow++;
            }

            report.Show();
        }
    }
}

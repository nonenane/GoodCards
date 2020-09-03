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
    }
}

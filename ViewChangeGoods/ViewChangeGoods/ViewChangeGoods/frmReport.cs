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
    public partial class frmReport : Form
    {
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormationOfRevaluation
{
    public partial class frmUpdatedGoods : Form
    {
        DataTable dtReqDetails;

        /// <summary>
        /// процедура инициализации формы
        /// </summary>
        /// <param name="_dtReqDetails">таблица со списком товаров, по которым уже имелись записи в таблице s_rcena_future и ценами по товарам</param>
        public frmUpdatedGoods(DataTable _dtReqDetails)
        {
            dtReqDetails = _dtReqDetails;
            InitializeComponent();
        }

        private void frmUpdatedGoods_Load(object sender, EventArgs e)
        {
            grdReqDetails.DataSource = null;
            grdReqDetails.AutoGenerateColumns = false;
            grdReqDetails.DataSource = dtReqDetails;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

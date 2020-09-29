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
        public frmViewGoods()
        {
            InitializeComponent();
        }
     
        private void frmViewGoods_Load(object sender, EventArgs e)
        {

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
    }
}

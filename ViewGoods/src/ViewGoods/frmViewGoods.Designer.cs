namespace ViewGoods
{
    partial class frmViewGoods
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btClose = new System.Windows.Forms.Button();
            this.btViewCartGoods = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbGrp3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGrp1 = new System.Windows.Forms.ComboBox();
            this.cmbGrp2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chbNewGoods = new System.Windows.Forms.CheckBox();
            this.btClear = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbEan = new System.Windows.Forms.TextBox();
            this.lFind = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGrp1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGrp2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGrp3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbReserv = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(1009, 599);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 0;
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // btViewCartGoods
            // 
            this.btViewCartGoods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btViewCartGoods.Location = new System.Drawing.Point(971, 599);
            this.btViewCartGoods.Name = "btViewCartGoods";
            this.btViewCartGoods.Size = new System.Drawing.Size(32, 32);
            this.btViewCartGoods.TabIndex = 0;
            this.btViewCartGoods.UseVisualStyleBackColor = true;
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Location = new System.Drawing.Point(933, 599);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 0;
            this.btPrint.UseVisualStyleBackColor = true;
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Location = new System.Drawing.Point(115, 12);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(208, 21);
            this.cmbShop.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Магазин:";
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(115, 39);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(208, 21);
            this.cmbDeps.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Отдел:";
            // 
            // cmbGrp3
            // 
            this.cmbGrp3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrp3.FormattingEnabled = true;
            this.cmbGrp3.Location = new System.Drawing.Point(115, 65);
            this.cmbGrp3.Name = "cmbGrp3";
            this.cmbGrp3.Size = new System.Drawing.Size(208, 21);
            this.cmbGrp3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Подгруппа:";
            // 
            // cmbGrp1
            // 
            this.cmbGrp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrp1.FormattingEnabled = true;
            this.cmbGrp1.Location = new System.Drawing.Point(416, 12);
            this.cmbGrp1.Name = "cmbGrp1";
            this.cmbGrp1.Size = new System.Drawing.Size(270, 21);
            this.cmbGrp1.TabIndex = 1;
            // 
            // cmbGrp2
            // 
            this.cmbGrp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrp2.FormattingEnabled = true;
            this.cmbGrp2.Location = new System.Drawing.Point(416, 39);
            this.cmbGrp2.Name = "cmbGrp2";
            this.cmbGrp2.Size = new System.Drawing.Size(270, 21);
            this.cmbGrp2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(343, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Т/У группа:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Инв. группа:";
            // 
            // chbNewGoods
            // 
            this.chbNewGoods.AutoSize = true;
            this.chbNewGoods.Location = new System.Drawing.Point(343, 65);
            this.chbNewGoods.Name = "chbNewGoods";
            this.chbNewGoods.Size = new System.Drawing.Size(100, 17);
            this.chbNewGoods.TabIndex = 3;
            this.chbNewGoods.Text = "Новые товары";
            this.chbNewGoods.UseVisualStyleBackColor = true;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(560, 99);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(32, 32);
            this.btClear.TabIndex = 0;
            this.btClear.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(450, 105);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 4;
            // 
            // tbEan
            // 
            this.tbEan.Location = new System.Drawing.Point(344, 105);
            this.tbEan.Name = "tbEan";
            this.tbEan.Size = new System.Drawing.Size(100, 20);
            this.tbEan.TabIndex = 4;
            // 
            // lFind
            // 
            this.lFind.AutoSize = true;
            this.lFind.Location = new System.Drawing.Point(300, 109);
            this.lFind.Name = "lFind";
            this.lFind.Size = new System.Drawing.Size(39, 13);
            this.lFind.TabIndex = 2;
            this.lFind.Text = "Поиск";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeps,
            this.cGrp1,
            this.cGrp2,
            this.cEan,
            this.cName,
            this.cPrice,
            this.cGrp3});
            this.dgvData.Location = new System.Drawing.Point(12, 137);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1029, 456);
            this.dgvData.TabIndex = 5;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            // 
            // cDeps
            // 
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            // 
            // cGrp1
            // 
            this.cGrp1.HeaderText = "Т/У группа";
            this.cGrp1.Name = "cGrp1";
            this.cGrp1.ReadOnly = true;
            // 
            // cGrp2
            // 
            this.cGrp2.HeaderText = "Инв. группа";
            this.cGrp2.Name = "cGrp2";
            this.cGrp2.ReadOnly = true;
            // 
            // cEan
            // 
            this.cEan.HeaderText = "EAN";
            this.cEan.Name = "cEan";
            this.cEan.ReadOnly = true;
            // 
            // cName
            // 
            this.cName.HeaderText = "Наименование товара";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cPrice
            // 
            this.cPrice.HeaderText = "Цена продажи";
            this.cPrice.Name = "cPrice";
            this.cPrice.ReadOnly = true;
            // 
            // cGrp3
            // 
            this.cGrp3.HeaderText = "Подгруппа";
            this.cGrp3.Name = "cGrp3";
            this.cGrp3.ReadOnly = true;
            // 
            // chbReserv
            // 
            this.chbReserv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbReserv.AutoSize = true;
            this.chbReserv.Location = new System.Drawing.Point(38, 608);
            this.chbReserv.Name = "chbReserv";
            this.chbReserv.Size = new System.Drawing.Size(80, 17);
            this.chbReserv.TabIndex = 3;
            this.chbReserv.Text = "checkBox1";
            this.chbReserv.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Location = new System.Drawing.Point(12, 608);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(17, 17);
            this.panel1.TabIndex = 6;
            // 
            // frmViewGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 643);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.tbEan);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.chbReserv);
            this.Controls.Add(this.chbNewGoods);
            this.Controls.Add(this.lFind);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbGrp2);
            this.Controls.Add(this.cmbGrp3);
            this.Controls.Add(this.cmbGrp1);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.cmbShop);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btViewCartGoods);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btClose);
            this.Name = "frmViewGoods";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewGoods_FormClosing);
            this.Load += new System.EventHandler(this.frmViewGoods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btViewCartGoods;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbGrp3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGrp1;
        private System.Windows.Forms.ComboBox cmbGrp2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chbNewGoods;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbEan;
        private System.Windows.Forms.Label lFind;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.CheckBox chbReserv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGrp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGrp2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGrp3;
    }
}


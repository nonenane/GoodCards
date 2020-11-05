namespace ViewChangeGoods
{
    partial class frmViewChangeGoods
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btPrint = new System.Windows.Forms.Button();
            this.btViewCartGoods = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbReserv = new System.Windows.Forms.CheckBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.tbEan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGrp1 = new System.Windows.Forms.ComboBox();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.cmbShop = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btUpdate = new System.Windows.Forms.Button();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNameBefore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNameAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGrp1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameGrp1Atfer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNdsBefore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNdsAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUlBefore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUlAfter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1012, 652);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btUpdate);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.dtpDate);
            this.tabPage1.Controls.Add(this.btPrint);
            this.tabPage1.Controls.Add(this.btViewCartGoods);
            this.tabPage1.Controls.Add(this.btClose);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.chbReserv);
            this.tabPage1.Controls.Add(this.dgvData);
            this.tabPage1.Controls.Add(this.tbFIO);
            this.tabPage1.Controls.Add(this.tbDate);
            this.tabPage1.Controls.Add(this.tbEan);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmbGrp1);
            this.tabPage1.Controls.Add(this.cmbDeps);
            this.tabPage1.Controls.Add(this.cmbShop);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1004, 626);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Изменение данных по товару";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Т/У группа:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(796, 15);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(97, 20);
            this.dtpDate.TabIndex = 16;
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Location = new System.Drawing.Point(888, 575);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 13;
            this.btPrint.UseVisualStyleBackColor = true;
            // 
            // btViewCartGoods
            // 
            this.btViewCartGoods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btViewCartGoods.Location = new System.Drawing.Point(926, 575);
            this.btViewCartGoods.Name = "btViewCartGoods";
            this.btViewCartGoods.Size = new System.Drawing.Size(32, 32);
            this.btViewCartGoods.TabIndex = 14;
            this.btViewCartGoods.UseVisualStyleBackColor = true;
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(964, 575);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 15;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(699, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Дата изменений";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 560);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Время отправления на кассу";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel1.Location = new System.Drawing.Point(5, 558);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(17, 17);
            this.panel1.TabIndex = 11;
            // 
            // chbReserv
            // 
            this.chbReserv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbReserv.AutoSize = true;
            this.chbReserv.Location = new System.Drawing.Point(31, 558);
            this.chbReserv.Name = "chbReserv";
            this.chbReserv.Size = new System.Drawing.Size(117, 17);
            this.chbReserv.TabIndex = 10;
            this.chbReserv.Text = "Резервые товары";
            this.chbReserv.UseVisualStyleBackColor = true;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeps,
            this.cEan,
            this.cNameBefore,
            this.cNameAfter,
            this.cGrp1,
            this.nameGrp1Atfer,
            this.cNdsBefore,
            this.cNdsAfter,
            this.cUlBefore,
            this.cUlAfter});
            this.dgvData.Location = new System.Drawing.Point(8, 112);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(988, 439);
            this.dgvData.TabIndex = 9;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // tbFIO
            // 
            this.tbFIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbFIO.Location = new System.Drawing.Point(327, 582);
            this.tbFIO.MaxLength = 13;
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.ReadOnly = true;
            this.tbFIO.Size = new System.Drawing.Size(188, 20);
            this.tbFIO.TabIndex = 8;
            // 
            // tbDate
            // 
            this.tbDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDate.Location = new System.Drawing.Point(327, 556);
            this.tbDate.MaxLength = 13;
            this.tbDate.Name = "tbDate";
            this.tbDate.ReadOnly = true;
            this.tbDate.Size = new System.Drawing.Size(188, 20);
            this.tbDate.TabIndex = 8;
            // 
            // tbEan
            // 
            this.tbEan.Location = new System.Drawing.Point(8, 86);
            this.tbEan.MaxLength = 13;
            this.tbEan.Name = "tbEan";
            this.tbEan.Size = new System.Drawing.Size(100, 20);
            this.tbEan.TabIndex = 8;
            this.tbEan.TextChanged += new System.EventHandler(this.tbEan_TextChanged);
            this.tbEan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbEan_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Отдел:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Магазин:";
            // 
            // cmbGrp1
            // 
            this.cmbGrp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrp1.FormattingEnabled = true;
            this.cmbGrp1.Location = new System.Drawing.Point(376, 15);
            this.cmbGrp1.Name = "cmbGrp1";
            this.cmbGrp1.Size = new System.Drawing.Size(270, 21);
            this.cmbGrp1.TabIndex = 3;
            this.cmbGrp1.SelectionChangeCommitted += new System.EventHandler(this.cmbGrp1_SelectionChangeCommitted);
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(75, 42);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(208, 21);
            this.cmbDeps.TabIndex = 4;
            this.cmbDeps.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // cmbShop
            // 
            this.cmbShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShop.FormattingEnabled = true;
            this.cmbShop.Location = new System.Drawing.Point(75, 15);
            this.cmbShop.Name = "cmbShop";
            this.cmbShop.Size = new System.Drawing.Size(208, 21);
            this.cmbShop.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1004, 626);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Изменение цены по товару";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1004, 626);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Новые товары";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(570, 582);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(281, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Visible = false;
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Location = new System.Drawing.Point(964, 19);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(32, 32);
            this.btUpdate.TabIndex = 19;
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
            // 
            // cDeps
            // 
            this.cDeps.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cDeps.DataPropertyName = "nameDep";
            this.cDeps.Frozen = true;
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.MinimumWidth = 90;
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            this.cDeps.Width = 90;
            // 
            // cEan
            // 
            this.cEan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cEan.DataPropertyName = "ean";
            this.cEan.Frozen = true;
            this.cEan.HeaderText = "EAN";
            this.cEan.MinimumWidth = 100;
            this.cEan.Name = "cEan";
            this.cEan.ReadOnly = true;
            // 
            // cNameBefore
            // 
            this.cNameBefore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNameBefore.DataPropertyName = "nameBefore";
            this.cNameBefore.HeaderText = "Наименование товара до";
            this.cNameBefore.MinimumWidth = 250;
            this.cNameBefore.Name = "cNameBefore";
            this.cNameBefore.ReadOnly = true;
            this.cNameBefore.Width = 250;
            // 
            // cNameAfter
            // 
            this.cNameAfter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNameAfter.DataPropertyName = "nameAtfer";
            this.cNameAfter.HeaderText = "Наименование товара после";
            this.cNameAfter.MinimumWidth = 250;
            this.cNameAfter.Name = "cNameAfter";
            this.cNameAfter.ReadOnly = true;
            this.cNameAfter.Width = 250;
            // 
            // cGrp1
            // 
            this.cGrp1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cGrp1.DataPropertyName = "nameGrp1Before";
            this.cGrp1.HeaderText = "Т/У товара до";
            this.cGrp1.MinimumWidth = 110;
            this.cGrp1.Name = "cGrp1";
            this.cGrp1.ReadOnly = true;
            this.cGrp1.Width = 110;
            // 
            // nameGrp1Atfer
            // 
            this.nameGrp1Atfer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nameGrp1Atfer.DataPropertyName = "nameGrp1After";
            this.nameGrp1Atfer.HeaderText = "Т/У товара после";
            this.nameGrp1Atfer.MinimumWidth = 110;
            this.nameGrp1Atfer.Name = "nameGrp1Atfer";
            this.nameGrp1Atfer.ReadOnly = true;
            this.nameGrp1Atfer.Width = 110;
            // 
            // cNdsBefore
            // 
            this.cNdsBefore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNdsBefore.DataPropertyName = "taxBefore";
            this.cNdsBefore.HeaderText = "НДС до";
            this.cNdsBefore.MinimumWidth = 80;
            this.cNdsBefore.Name = "cNdsBefore";
            this.cNdsBefore.ReadOnly = true;
            this.cNdsBefore.Width = 80;
            // 
            // cNdsAfter
            // 
            this.cNdsAfter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNdsAfter.DataPropertyName = "taxAtfer";
            this.cNdsAfter.HeaderText = "НДС после";
            this.cNdsAfter.MinimumWidth = 80;
            this.cNdsAfter.Name = "cNdsAfter";
            this.cNdsAfter.ReadOnly = true;
            this.cNdsAfter.Width = 80;
            // 
            // cUlBefore
            // 
            this.cUlBefore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cUlBefore.DataPropertyName = "ulBefore";
            this.cUlBefore.HeaderText = "ЮЛ до";
            this.cUlBefore.MinimumWidth = 70;
            this.cUlBefore.Name = "cUlBefore";
            this.cUlBefore.ReadOnly = true;
            this.cUlBefore.Width = 70;
            // 
            // cUlAfter
            // 
            this.cUlAfter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cUlAfter.DataPropertyName = "ulAfter";
            this.cUlAfter.HeaderText = "ЮЛ после";
            this.cUlAfter.MinimumWidth = 70;
            this.cUlAfter.Name = "cUlAfter";
            this.cUlAfter.ReadOnly = true;
            this.cUlAfter.Width = 70;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 582);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Оператор";
            // 
            // frmViewChangeGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 652);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmViewChangeGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение товара";
            this.Load += new System.EventHandler(this.frmViewChangeGoods_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox tbEan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGrp1;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.ComboBox cmbShop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbReserv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btViewCartGoods;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEan;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameBefore;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameAfter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGrp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameGrp1Atfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNdsBefore;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNdsAfter;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUlBefore;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUlAfter;
        private System.Windows.Forms.Label label6;
    }
}


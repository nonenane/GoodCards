namespace dllGoodCardDicGrp2
{
    partial class frmList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.btPrint = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbNotActive = new System.Windows.Forms.CheckBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cDeps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUniGrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cReglam = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cLimitTovar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cLimitInut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cLimitDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbName = new System.Windows.Forms.TextBox();
            this.chbReglam = new System.Windows.Forms.CheckBox();
            this.chbLimitTovar = new System.Windows.Forms.CheckBox();
            this.cmbUniGrp = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gTypeGrp = new System.Windows.Forms.GroupBox();
            this.rbNetto = new System.Windows.Forms.RadioButton();
            this.rbUnit = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gTypeGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Отдел";
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(50, 4);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(206, 21);
            this.cmbDeps.TabIndex = 21;
            this.cmbDeps.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::dllGoodCardDicGrp2.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(974, 586);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 20;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Image = global::dllGoodCardDicGrp2.Properties.Resources.Add;
            this.btAdd.Location = new System.Drawing.Point(1055, 586);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(32, 32);
            this.btAdd.TabIndex = 16;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.Image = global::dllGoodCardDicGrp2.Properties.Resources.Edit;
            this.btEdit.Location = new System.Drawing.Point(1093, 586);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(32, 32);
            this.btEdit.TabIndex = 17;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Image = global::dllGoodCardDicGrp2.Properties.Resources.Trash;
            this.btDelete.Location = new System.Drawing.Point(1131, 586);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(32, 32);
            this.btDelete.TabIndex = 18;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllGoodCardDicGrp2.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(1169, 586);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 19;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(6, 593);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 21);
            this.panel1.TabIndex = 15;
            // 
            // chbNotActive
            // 
            this.chbNotActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbNotActive.AutoSize = true;
            this.chbNotActive.Location = new System.Drawing.Point(33, 595);
            this.chbNotActive.Name = "chbNotActive";
            this.chbNotActive.Size = new System.Drawing.Size(113, 17);
            this.chbNotActive.TabIndex = 14;
            this.chbNotActive.Text = "- недействующие";
            this.chbNotActive.UseVisualStyleBackColor = true;
            this.chbNotActive.CheckedChanged += new System.EventHandler(this.chbNotActive_CheckedChanged);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeps,
            this.cName,
            this.cUniGrp,
            this.cReglam,
            this.cLimitTovar,
            this.cLimitInut,
            this.cLimitDay,
            this.cUnit});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.Location = new System.Drawing.Point(12, 61);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1189, 519);
            this.dgvData.TabIndex = 13;
            this.dgvData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvData_ColumnWidthChanged);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvData_RowPrePaint);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // cDeps
            // 
            this.cDeps.DataPropertyName = "nameDeps";
            this.cDeps.HeaderText = "Отдел";
            this.cDeps.Name = "cDeps";
            this.cDeps.ReadOnly = true;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Наименование";
            this.cName.MinimumWidth = 110;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.Width = 300;
            // 
            // cUniGrp
            // 
            this.cUniGrp.DataPropertyName = "nameUniGrp";
            this.cUniGrp.HeaderText = "Группа для инвентаризации";
            this.cUniGrp.Name = "cUniGrp";
            this.cUniGrp.ReadOnly = true;
            // 
            // cReglam
            // 
            this.cReglam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cReglam.DataPropertyName = "specification";
            this.cReglam.HeaderText = "Соотв. тех. регламенту";
            this.cReglam.MinimumWidth = 75;
            this.cReglam.Name = "cReglam";
            this.cReglam.ReadOnly = true;
            this.cReglam.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cReglam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cReglam.Width = 110;
            // 
            // cLimitTovar
            // 
            this.cLimitTovar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cLimitTovar.DataPropertyName = "skoroportovar";
            this.cLimitTovar.HeaderText = "Скоропортящийся товар";
            this.cLimitTovar.MinimumWidth = 75;
            this.cLimitTovar.Name = "cLimitTovar";
            this.cLimitTovar.ReadOnly = true;
            this.cLimitTovar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cLimitTovar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cLimitTovar.Width = 110;
            // 
            // cLimitInut
            // 
            this.cLimitInut.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cLimitInut.DataPropertyName = "NettoMax";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cLimitInut.DefaultCellStyle = dataGridViewCellStyle2;
            this.cLimitInut.HeaderText = "Огр. в штуках на заказ";
            this.cLimitInut.MinimumWidth = 75;
            this.cLimitInut.Name = "cLimitInut";
            this.cLimitInut.ReadOnly = true;
            this.cLimitInut.Width = 110;
            // 
            // cLimitDay
            // 
            this.cLimitDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cLimitDay.DataPropertyName = "DayMax";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cLimitDay.DefaultCellStyle = dataGridViewCellStyle3;
            this.cLimitDay.HeaderText = "Огр. в днях на заказ";
            this.cLimitDay.MinimumWidth = 70;
            this.cLimitDay.Name = "cLimitDay";
            this.cLimitDay.ReadOnly = true;
            // 
            // cUnit
            // 
            this.cUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cUnit.DataPropertyName = "nameUnit";
            this.cUnit.HeaderText = "Тип группы";
            this.cUnit.MinimumWidth = 55;
            this.cUnit.Name = "cUnit";
            this.cUnit.ReadOnly = true;
            this.cUnit.Width = 55;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 35);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(244, 20);
            this.tbName.TabIndex = 12;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // chbReglam
            // 
            this.chbReglam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbReglam.AutoSize = true;
            this.chbReglam.Location = new System.Drawing.Point(191, 586);
            this.chbReglam.Name = "chbReglam";
            this.chbReglam.Size = new System.Drawing.Size(148, 17);
            this.chbReglam.TabIndex = 14;
            this.chbReglam.Text = "- соотв. тех. регламенту";
            this.chbReglam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbReglam.UseVisualStyleBackColor = true;
            this.chbReglam.CheckedChanged += new System.EventHandler(this.chbNotActive_CheckedChanged);
            // 
            // chbLimitTovar
            // 
            this.chbLimitTovar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbLimitTovar.AutoSize = true;
            this.chbLimitTovar.Location = new System.Drawing.Point(191, 609);
            this.chbLimitTovar.Name = "chbLimitTovar";
            this.chbLimitTovar.Size = new System.Drawing.Size(156, 17);
            this.chbLimitTovar.TabIndex = 14;
            this.chbLimitTovar.Text = "- скоропортящийся товар";
            this.chbLimitTovar.UseVisualStyleBackColor = true;
            this.chbLimitTovar.CheckedChanged += new System.EventHandler(this.chbNotActive_CheckedChanged);
            // 
            // cmbUniGrp
            // 
            this.cmbUniGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUniGrp.FormattingEnabled = true;
            this.cmbUniGrp.Location = new System.Drawing.Point(378, 5);
            this.cmbUniGrp.Name = "cmbUniGrp";
            this.cmbUniGrp.Size = new System.Drawing.Size(206, 21);
            this.cmbUniGrp.TabIndex = 21;
            this.cmbUniGrp.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Группа для инв.";
            // 
            // gTypeGrp
            // 
            this.gTypeGrp.Controls.Add(this.rbNetto);
            this.gTypeGrp.Controls.Add(this.rbUnit);
            this.gTypeGrp.Controls.Add(this.rbAll);
            this.gTypeGrp.Location = new System.Drawing.Point(608, 5);
            this.gTypeGrp.Name = "gTypeGrp";
            this.gTypeGrp.Size = new System.Drawing.Size(220, 50);
            this.gTypeGrp.TabIndex = 23;
            this.gTypeGrp.TabStop = false;
            this.gTypeGrp.Text = "Тип группы";
            // 
            // rbNetto
            // 
            this.rbNetto.AutoSize = true;
            this.rbNetto.Location = new System.Drawing.Point(141, 19);
            this.rbNetto.Name = "rbNetto";
            this.rbNetto.Size = new System.Drawing.Size(68, 17);
            this.rbNetto.TabIndex = 0;
            this.rbNetto.Text = "Весовой";
            this.rbNetto.UseVisualStyleBackColor = true;
            this.rbNetto.Click += new System.EventHandler(this.rbAll_Click);
            // 
            // rbUnit
            // 
            this.rbUnit.AutoSize = true;
            this.rbUnit.Location = new System.Drawing.Point(66, 19);
            this.rbUnit.Name = "rbUnit";
            this.rbUnit.Size = new System.Drawing.Size(69, 17);
            this.rbUnit.TabIndex = 0;
            this.rbUnit.Text = "Штучный";
            this.rbUnit.UseVisualStyleBackColor = true;
            this.rbUnit.Click += new System.EventHandler(this.rbAll_Click);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(16, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(44, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "Все";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.Click += new System.EventHandler(this.rbAll_Click);
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 630);
            this.Controls.Add(this.gTypeGrp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbUniGrp);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chbLimitTovar);
            this.Controls.Add(this.chbReglam);
            this.Controls.Add(this.chbNotActive);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.tbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник инв групп";
            this.Load += new System.EventHandler(this.frmList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gTypeGrp.ResumeLayout(false);
            this.gTypeGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbNotActive;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox chbReglam;
        private System.Windows.Forms.CheckBox chbLimitTovar;
        private System.Windows.Forms.ComboBox cmbUniGrp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gTypeGrp;
        private System.Windows.Forms.RadioButton rbNetto;
        private System.Windows.Forms.RadioButton rbUnit;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeps;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUniGrp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cReglam;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cLimitTovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLimitInut;
        private System.Windows.Forms.DataGridViewTextBoxColumn cLimitDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUnit;
    }
}


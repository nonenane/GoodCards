namespace dllGoodCardDicTuGrp
{
    partial class frmAdd
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.cmbNds = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbWithSubGroups = new System.Windows.Forms.CheckBox();
            this.chbIsCredit = new System.Windows.Forms.CheckBox();
            this.tbNaneGrp = new System.Windows.Forms.TextBox();
            this.dgvAdress = new System.Windows.Forms.DataGridView();
            this.cV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cNameGrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdress)).BeginInit();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(101, 35);
            this.tbName.MaxLength = 100;
            this.tbName.Name = "tbName";
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbName.Size = new System.Drawing.Size(377, 20);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(15, 39);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(83, 13);
            this.lName.TabIndex = 6;
            this.lName.Text = "Наименование";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::dllGoodCardDicTuGrp.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(409, 491);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 2;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllGoodCardDicTuGrp.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(447, 491);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 3;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Отдел";
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(101, 60);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(377, 21);
            this.cmbDeps.TabIndex = 12;
            this.cmbDeps.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // cmbNds
            // 
            this.cmbNds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNds.FormattingEnabled = true;
            this.cmbNds.Location = new System.Drawing.Point(101, 87);
            this.cmbNds.Name = "cmbNds";
            this.cmbNds.Size = new System.Drawing.Size(377, 21);
            this.cmbNds.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "НДС";
            // 
            // chbWithSubGroups
            // 
            this.chbWithSubGroups.Location = new System.Drawing.Point(15, 139);
            this.chbWithSubGroups.Name = "chbWithSubGroups";
            this.chbWithSubGroups.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbWithSubGroups.Size = new System.Drawing.Size(463, 17);
            this.chbWithSubGroups.TabIndex = 14;
            this.chbWithSubGroups.Text = "Наличие подгруппы";
            this.chbWithSubGroups.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbWithSubGroups.UseVisualStyleBackColor = true;
            // 
            // chbIsCredit
            // 
            this.chbIsCredit.Location = new System.Drawing.Point(15, 116);
            this.chbIsCredit.Name = "chbIsCredit";
            this.chbIsCredit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbIsCredit.Size = new System.Drawing.Size(463, 17);
            this.chbIsCredit.TabIndex = 15;
            this.chbIsCredit.Text = "Признак кредитования";
            this.chbIsCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbIsCredit.UseVisualStyleBackColor = true;
            // 
            // tbNaneGrp
            // 
            this.tbNaneGrp.Location = new System.Drawing.Point(153, 198);
            this.tbNaneGrp.Name = "tbNaneGrp";
            this.tbNaneGrp.Size = new System.Drawing.Size(326, 20);
            this.tbNaneGrp.TabIndex = 16;
            this.tbNaneGrp.TextChanged += new System.EventHandler(this.tbNaneGrp_TextChanged);
            // 
            // dgvAdress
            // 
            this.dgvAdress.AllowUserToAddRows = false;
            this.dgvAdress.AllowUserToDeleteRows = false;
            this.dgvAdress.AllowUserToResizeRows = false;
            this.dgvAdress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAdress.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdress.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAdress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cV,
            this.cNameGrp});
            this.dgvAdress.Location = new System.Drawing.Point(101, 224);
            this.dgvAdress.MultiSelect = false;
            this.dgvAdress.Name = "dgvAdress";
            this.dgvAdress.RowHeadersVisible = false;
            this.dgvAdress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdress.Size = new System.Drawing.Size(378, 261);
            this.dgvAdress.TabIndex = 17;
            this.dgvAdress.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvAdress_ColumnWidthChanged);
            // 
            // cV
            // 
            this.cV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cV.DataPropertyName = "isSelect";
            this.cV.FillWeight = 45.68528F;
            this.cV.HeaderText = "V";
            this.cV.MinimumWidth = 45;
            this.cV.Name = "cV";
            this.cV.Width = 45;
            // 
            // cNameGrp
            // 
            this.cNameGrp.DataPropertyName = "cName";
            this.cNameGrp.FillWeight = 154.3147F;
            this.cNameGrp.HeaderText = "Связанные инв. группы";
            this.cNameGrp.MinimumWidth = 20;
            this.cNameGrp.Name = "cNameGrp";
            this.cNameGrp.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Связь Т/У группы с инв. группами.";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(101, 9);
            this.tbID.MaxLength = 20;
            this.tbID.Name = "tbID";
            this.tbID.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbID.Size = new System.Drawing.Size(98, 20);
            this.tbID.TabIndex = 0;
            this.tbID.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbID_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "ID";
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 535);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvAdress);
            this.Controls.Add(this.tbNaneGrp);
            this.Controls.Add(this.chbWithSubGroups);
            this.Controls.Add(this.chbIsCredit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbNds);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.ComboBox cmbNds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbWithSubGroups;
        private System.Windows.Forms.CheckBox chbIsCredit;
        private System.Windows.Forms.TextBox tbNaneGrp;
        private System.Windows.Forms.DataGridView dgvAdress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cV;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameGrp;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label label4;
    }
}
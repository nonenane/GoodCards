namespace dllGoodCardDicTuGrp
{
    partial class frmViewMngTuGrp
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
            this.tbNaneGrp = new System.Windows.Forms.TextBox();
            this.dgvAdress = new System.Windows.Forms.DataGridView();
            this.cNameGrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btPrint = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.tbNameGrp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdress)).BeginInit();
            this.SuspendLayout();
            // 
            // tbNaneGrp
            // 
            this.tbNaneGrp.Location = new System.Drawing.Point(12, 38);
            this.tbNaneGrp.Name = "tbNaneGrp";
            this.tbNaneGrp.Size = new System.Drawing.Size(146, 20);
            this.tbNaneGrp.TabIndex = 12;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAdress.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAdress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNameGrp});
            this.dgvAdress.Location = new System.Drawing.Point(12, 64);
            this.dgvAdress.MultiSelect = false;
            this.dgvAdress.Name = "dgvAdress";
            this.dgvAdress.ReadOnly = true;
            this.dgvAdress.RowHeadersVisible = false;
            this.dgvAdress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdress.Size = new System.Drawing.Size(374, 448);
            this.dgvAdress.TabIndex = 11;
            this.dgvAdress.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvAdress_ColumnWidthChanged);
            // 
            // cNameGrp
            // 
            this.cNameGrp.DataPropertyName = "FIO";
            this.cNameGrp.HeaderText = "ФИО менеджера";
            this.cNameGrp.MinimumWidth = 20;
            this.cNameGrp.Name = "cNameGrp";
            this.cNameGrp.ReadOnly = true;
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.Image = global::dllGoodCardDicTuGrp.Properties.Resources.klpq_2511;
            this.btPrint.Location = new System.Drawing.Point(324, 528);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 10;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllGoodCardDicTuGrp.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(362, 528);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 9;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // tbNameGrp
            // 
            this.tbNameGrp.Enabled = false;
            this.tbNameGrp.Location = new System.Drawing.Point(85, 12);
            this.tbNameGrp.Name = "tbNameGrp";
            this.tbNameGrp.Size = new System.Drawing.Size(309, 20);
            this.tbNameGrp.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Т/У группа:";
            // 
            // frmViewMngTuGrp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 564);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNameGrp);
            this.Controls.Add(this.tbNaneGrp);
            this.Controls.Add(this.dgvAdress);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewMngTuGrp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр менеджеров Т/У групп";
            this.Load += new System.EventHandler(this.frmViewMngTuGrp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.TextBox tbNaneGrp;
        private System.Windows.Forms.DataGridView dgvAdress;
        private System.Windows.Forms.TextBox tbNameGrp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNameGrp;
    }
}
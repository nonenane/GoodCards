namespace FormationOfRevaluation
{
    partial class frmUpdatedGoods
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.grdReqDetails = new System.Windows.Forms.DataGridView();
            this.npp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_req = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_treq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tovar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.old_rcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zcenabnds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rcena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdReqDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(283, 225);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 168;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grdReqDetails
            // 
            this.grdReqDetails.AllowUserToAddRows = false;
            this.grdReqDetails.AllowUserToDeleteRows = false;
            this.grdReqDetails.AllowUserToResizeRows = false;
            this.grdReqDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdReqDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdReqDetails.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdReqDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdReqDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReqDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.npp,
            this.id_req,
            this.id_treq,
            this.id_tovar,
            this.ean,
            this.cname,
            this.netto,
            this.old_rcena,
            this.zcenabnds,
            this.rcena});
            this.grdReqDetails.Location = new System.Drawing.Point(12, 12);
            this.grdReqDetails.MultiSelect = false;
            this.grdReqDetails.Name = "grdReqDetails";
            this.grdReqDetails.ReadOnly = true;
            this.grdReqDetails.RowHeadersVisible = false;
            this.grdReqDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdReqDetails.Size = new System.Drawing.Size(629, 207);
            this.grdReqDetails.TabIndex = 169;
            this.grdReqDetails.TabStop = false;
            // 
            // npp
            // 
            this.npp.DataPropertyName = "npp";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.npp.DefaultCellStyle = dataGridViewCellStyle2;
            this.npp.FillWeight = 50F;
            this.npp.HeaderText = "№";
            this.npp.Name = "npp";
            this.npp.ReadOnly = true;
            this.npp.Visible = false;
            // 
            // id_req
            // 
            this.id_req.DataPropertyName = "id_req";
            this.id_req.HeaderText = "id_req";
            this.id_req.Name = "id_req";
            this.id_req.ReadOnly = true;
            this.id_req.Visible = false;
            // 
            // id_treq
            // 
            this.id_treq.DataPropertyName = "id_treq";
            this.id_treq.HeaderText = "id_treq";
            this.id_treq.Name = "id_treq";
            this.id_treq.ReadOnly = true;
            this.id_treq.Visible = false;
            // 
            // id_tovar
            // 
            this.id_tovar.DataPropertyName = "id_tovar";
            this.id_tovar.HeaderText = "id_tovar";
            this.id_tovar.Name = "id_tovar";
            this.id_tovar.ReadOnly = true;
            this.id_tovar.Visible = false;
            // 
            // ean
            // 
            this.ean.DataPropertyName = "ean";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ean.DefaultCellStyle = dataGridViewCellStyle3;
            this.ean.FillWeight = 150F;
            this.ean.HeaderText = "EAN";
            this.ean.Name = "ean";
            this.ean.ReadOnly = true;
            // 
            // cname
            // 
            this.cname.DataPropertyName = "cname";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cname.DefaultCellStyle = dataGridViewCellStyle4;
            this.cname.FillWeight = 300F;
            this.cname.HeaderText = "Наименование товара";
            this.cname.Name = "cname";
            this.cname.ReadOnly = true;
            // 
            // netto
            // 
            this.netto.DataPropertyName = "netto";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.netto.DefaultCellStyle = dataGridViewCellStyle5;
            this.netto.HeaderText = "Кол-во";
            this.netto.Name = "netto";
            this.netto.ReadOnly = true;
            this.netto.Visible = false;
            // 
            // old_rcena
            // 
            this.old_rcena.DataPropertyName = "old_rcena";
            this.old_rcena.HeaderText = "Цена, которая была";
            this.old_rcena.Name = "old_rcena";
            this.old_rcena.ReadOnly = true;
            // 
            // zcenabnds
            // 
            this.zcenabnds.DataPropertyName = "zcenabnds";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.zcenabnds.DefaultCellStyle = dataGridViewCellStyle6;
            this.zcenabnds.HeaderText = "Новая цена переоценки";
            this.zcenabnds.Name = "zcenabnds";
            this.zcenabnds.ReadOnly = true;
            // 
            // rcena
            // 
            this.rcena.DataPropertyName = "rcena";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.rcena.DefaultCellStyle = dataGridViewCellStyle7;
            this.rcena.HeaderText = "Текущая цена";
            this.rcena.Name = "rcena";
            this.rcena.ReadOnly = true;
            this.rcena.Visible = false;
            // 
            // frmUpdatedGoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 260);
            this.ControlBox = false;
            this.Controls.Add(this.grdReqDetails);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdatedGoods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Результат переоценки";
            this.Load += new System.EventHandler(this.frmUpdatedGoods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdReqDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridView grdReqDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn npp;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_req;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_treq;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tovar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ean;
        private System.Windows.Forms.DataGridViewTextBoxColumn cname;
        private System.Windows.Forms.DataGridViewTextBoxColumn netto;
        private System.Windows.Forms.DataGridViewTextBoxColumn old_rcena;
        private System.Windows.Forms.DataGridViewTextBoxColumn zcenabnds;
        private System.Windows.Forms.DataGridViewTextBoxColumn rcena;
    }
}
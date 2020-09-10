namespace dllGoodCardDicCreaters
{
    partial class frmAddAdres
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btDicSubject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(101, 38);
            this.tbName.MaxLength = 200;
            this.tbName.Multiline = true;
            this.tbName.Name = "tbName";
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbName.Size = new System.Drawing.Size(377, 101);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(57, 38);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(38, 13);
            this.lName.TabIndex = 6;
            this.lName.Text = "Адрес";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(101, 8);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(330, 21);
            this.cmbSubject.TabIndex = 9;
            this.cmbSubject.SelectionChangeCommitted += new System.EventHandler(this.cmbSubject_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Страна\\Субъект";
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::dllGoodCardDicCreaters.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(408, 146);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 2;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllGoodCardDicCreaters.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(446, 146);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 3;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btDicSubject
            // 
            this.btDicSubject.Location = new System.Drawing.Point(440, 8);
            this.btDicSubject.Name = "btDicSubject";
            this.btDicSubject.Size = new System.Drawing.Size(38, 19);
            this.btDicSubject.TabIndex = 11;
            this.btDicSubject.Text = "...";
            this.btDicSubject.UseVisualStyleBackColor = true;
            this.btDicSubject.Click += new System.EventHandler(this.btDicSubject_Click);
            // 
            // frmAddAdres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 190);
            this.Controls.Add(this.btDicSubject);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddAdres";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btDicSubject;
    }
}
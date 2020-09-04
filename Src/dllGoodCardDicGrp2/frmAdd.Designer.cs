namespace dllGoodCardDicGrp2
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
            this.lDeps = new System.Windows.Forms.Label();
            this.cmbDeps = new System.Windows.Forms.ComboBox();
            this.lName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lUniGrp = new System.Windows.Forms.Label();
            this.cmbUniGrp = new System.Windows.Forms.ComboBox();
            this.chbLimitTovar = new System.Windows.Forms.CheckBox();
            this.chbReglam = new System.Windows.Forms.CheckBox();
            this.lNetto = new System.Windows.Forms.Label();
            this.tbUnit = new System.Windows.Forms.TextBox();
            this.lDay = new System.Windows.Forms.Label();
            this.tbDays = new System.Windows.Forms.TextBox();
            this.rbNetto = new System.Windows.Forms.RadioButton();
            this.rbUnit = new System.Windows.Forms.RadioButton();
            this.lTypeUnit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lDeps
            // 
            this.lDeps.AutoSize = true;
            this.lDeps.Location = new System.Drawing.Point(15, 46);
            this.lDeps.Name = "lDeps";
            this.lDeps.Size = new System.Drawing.Size(38, 13);
            this.lDeps.TabIndex = 17;
            this.lDeps.Text = "Отдел";
            // 
            // cmbDeps
            // 
            this.cmbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeps.FormattingEnabled = true;
            this.cmbDeps.Location = new System.Drawing.Point(113, 42);
            this.cmbDeps.Name = "cmbDeps";
            this.cmbDeps.Size = new System.Drawing.Size(357, 21);
            this.cmbDeps.TabIndex = 16;
            this.cmbDeps.SelectionChangeCommitted += new System.EventHandler(this.cmbDeps_SelectionChangeCommitted);
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(15, 15);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(83, 13);
            this.lName.TabIndex = 15;
            this.lName.Text = "Наименование";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(113, 12);
            this.tbName.MaxLength = 100;
            this.tbName.Name = "tbName";
            this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbName.Size = new System.Drawing.Size(357, 20);
            this.tbName.TabIndex = 14;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Image = global::dllGoodCardDicGrp2.Properties.Resources.Save;
            this.btSave.Location = new System.Drawing.Point(400, 223);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 18;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::dllGoodCardDicGrp2.Properties.Resources.Exit;
            this.btClose.Location = new System.Drawing.Point(438, 223);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 19;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lUniGrp
            // 
            this.lUniGrp.AutoSize = true;
            this.lUniGrp.Location = new System.Drawing.Point(15, 73);
            this.lUniGrp.Name = "lUniGrp";
            this.lUniGrp.Size = new System.Drawing.Size(87, 13);
            this.lUniGrp.TabIndex = 24;
            this.lUniGrp.Text = "Группа для инв.";
            // 
            // cmbUniGrp
            // 
            this.cmbUniGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUniGrp.FormattingEnabled = true;
            this.cmbUniGrp.Location = new System.Drawing.Point(113, 69);
            this.cmbUniGrp.Name = "cmbUniGrp";
            this.cmbUniGrp.Size = new System.Drawing.Size(186, 21);
            this.cmbUniGrp.TabIndex = 23;
            // 
            // chbLimitTovar
            // 
            this.chbLimitTovar.Location = new System.Drawing.Point(15, 119);
            this.chbLimitTovar.Name = "chbLimitTovar";
            this.chbLimitTovar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbLimitTovar.Size = new System.Drawing.Size(454, 17);
            this.chbLimitTovar.TabIndex = 25;
            this.chbLimitTovar.Text = "Скоропортящийся товар";
            this.chbLimitTovar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbLimitTovar.UseVisualStyleBackColor = true;
            // 
            // chbReglam
            // 
            this.chbReglam.Location = new System.Drawing.Point(15, 96);
            this.chbReglam.Name = "chbReglam";
            this.chbReglam.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chbReglam.Size = new System.Drawing.Size(454, 17);
            this.chbReglam.TabIndex = 26;
            this.chbReglam.Text = "Соответствующий техническому регламенту";
            this.chbReglam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbReglam.UseVisualStyleBackColor = true;
            // 
            // lNetto
            // 
            this.lNetto.AutoSize = true;
            this.lNetto.Location = new System.Drawing.Point(15, 148);
            this.lNetto.Name = "lNetto";
            this.lNetto.Size = new System.Drawing.Size(260, 13);
            this.lNetto.TabIndex = 27;
            this.lNetto.Text = "Ограничение в штуках на группу  на заказ товара";
            // 
            // tbUnit
            // 
            this.tbUnit.Location = new System.Drawing.Point(366, 144);
            this.tbUnit.MaxLength = 12;
            this.tbUnit.Name = "tbUnit";
            this.tbUnit.Size = new System.Drawing.Size(100, 20);
            this.tbUnit.TabIndex = 28;
            this.tbUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUnit_KeyPress);
            // 
            // lDay
            // 
            this.lDay.AutoSize = true;
            this.lDay.Location = new System.Drawing.Point(15, 174);
            this.lDay.Name = "lDay";
            this.lDay.Size = new System.Drawing.Size(245, 13);
            this.lDay.TabIndex = 27;
            this.lDay.Text = "Ограничение в днях на группу на заказ товара";
            // 
            // tbDays
            // 
            this.tbDays.Location = new System.Drawing.Point(366, 170);
            this.tbDays.MaxLength = 3;
            this.tbDays.Name = "tbDays";
            this.tbDays.Size = new System.Drawing.Size(100, 20);
            this.tbDays.TabIndex = 28;
            this.tbDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDays_KeyPress);
            // 
            // rbNetto
            // 
            this.rbNetto.AutoSize = true;
            this.rbNetto.Location = new System.Drawing.Point(398, 196);
            this.rbNetto.Name = "rbNetto";
            this.rbNetto.Size = new System.Drawing.Size(68, 17);
            this.rbNetto.TabIndex = 29;
            this.rbNetto.Text = "Весовой";
            this.rbNetto.UseVisualStyleBackColor = true;
            // 
            // rbUnit
            // 
            this.rbUnit.AutoSize = true;
            this.rbUnit.Location = new System.Drawing.Point(323, 196);
            this.rbUnit.Name = "rbUnit";
            this.rbUnit.Size = new System.Drawing.Size(69, 17);
            this.rbUnit.TabIndex = 30;
            this.rbUnit.Text = "Штучный";
            this.rbUnit.UseVisualStyleBackColor = true;
            // 
            // lTypeUnit
            // 
            this.lTypeUnit.AutoSize = true;
            this.lTypeUnit.Location = new System.Drawing.Point(15, 198);
            this.lTypeUnit.Name = "lTypeUnit";
            this.lTypeUnit.Size = new System.Drawing.Size(64, 13);
            this.lTypeUnit.TabIndex = 27;
            this.lTypeUnit.Text = "Вид товара";
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 263);
            this.ControlBox = false;
            this.Controls.Add(this.rbNetto);
            this.Controls.Add(this.rbUnit);
            this.Controls.Add(this.tbDays);
            this.Controls.Add(this.lTypeUnit);
            this.Controls.Add(this.lDay);
            this.Controls.Add(this.tbUnit);
            this.Controls.Add(this.lNetto);
            this.Controls.Add(this.chbLimitTovar);
            this.Controls.Add(this.chbReglam);
            this.Controls.Add(this.lUniGrp);
            this.Controls.Add(this.cmbUniGrp);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.lDeps);
            this.Controls.Add(this.cmbDeps);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.tbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDeps;
        private System.Windows.Forms.ComboBox cmbDeps;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lUniGrp;
        private System.Windows.Forms.ComboBox cmbUniGrp;
        private System.Windows.Forms.CheckBox chbLimitTovar;
        private System.Windows.Forms.CheckBox chbReglam;
        private System.Windows.Forms.Label lNetto;
        private System.Windows.Forms.Label lDay;
        private System.Windows.Forms.TextBox tbDays;
        private System.Windows.Forms.RadioButton rbNetto;
        private System.Windows.Forms.RadioButton rbUnit;
        private System.Windows.Forms.Label lTypeUnit;
        private System.Windows.Forms.TextBox tbUnit;
    }
}
﻿using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllGoodCardDicGrp3
{
    public partial class frmAdd : Form
    {
        public DataRowView row { set; private get; }
        private bool isEditData = false;
        private string oldName, oldCode;
        private int id = 0;
        public frmAdd()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSave, "Сохранить");
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            Task<DataTable> task = Config.hCntMain.getDepartments(false);
            task.Wait();
            DataTable dtObjectLease = task.Result;

            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";
            cmbDeps.DataSource = dtObjectLease;
            cmbDeps.SelectedIndex = -1;

            if (row != null)
            {
                id = (int)row["id"];
                tbName.Text = (string)row["cName"];
                oldName = tbName.Text.Trim();

                cmbDeps.SelectedValue = row["id_otdel"];
                cmbDeps.Enabled = false;
            }
            else {
                cmbDeps.SelectedValue = UserSettings.User.IdDepartment;
                cmbDeps.Enabled = false;
            }
                isEditData = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (cmbDeps.SelectedValue == null)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{label1.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeps.Focus();
                return;
            }

            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lName.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            string cName = tbName.Text.Trim();
            int id_otdel = (int)cmbDeps.SelectedValue;
            bool isActive = true;
            bool isDel = false;
            int result = 0;
            bool isAutoIncriments = false;

            Task<DataTable> task = Config.hCntMain.setGrp3(id, cName, id_otdel, isActive, isDel, result, isAutoIncriments);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует подгруппа с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show($"{dtResult.Rows[0]["msg"]}", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            id = (int)dtResult.Rows[0]["id"];

            isAutoIncriments = true;
            task = Config.hCntSecond.setGrp3(id, cName, id_otdel, isActive, isDel, result, isAutoIncriments);
            task.Wait();

            if (id == 0)
            {
                id = (int)dtResult.Rows[0]["id"];
                Logging.StartFirstLevel(1564);
                //Logging.Comment("Добавить Тип документа");
                Logging.Comment($"ID: {id}");
                Logging.Comment($"Наименование: {tbName.Text.Trim()}");
                Logging.StopFirstLevel();
            }
            else
            {
                Logging.StartFirstLevel(1565);
                //Logging.Comment("Редактировать Тип документа");
                Logging.Comment($"ID: {id}");
                Logging.VariableChange("Наименование", tbName.Text.Trim(), oldName);
                Logging.StopFirstLevel();
            }

            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void cmbDeps_SelectionChangeCommitted(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}

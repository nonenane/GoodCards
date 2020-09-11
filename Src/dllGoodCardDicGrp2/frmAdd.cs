using Nwuram.Framework.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dllGoodCardDicGrp2
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

            task = Config.hCntMain.getUniGrp(false);
            task.Wait();
            DataTable dtUniGrp = task.Result;

            cmbUniGrp.DisplayMember = "cName";
            cmbUniGrp.ValueMember = "id";
            cmbUniGrp.DataSource = dtUniGrp;
            cmbUniGrp.SelectedIndex = -1;

            if (row != null)
            {
                id = (int)row["id"];
                tbName.Text = (string)row["cName"];
                oldName = tbName.Text.Trim();

                cmbDeps.SelectedValue = row["id_otdel"];
                cmbDeps.Enabled = false;

                cmbUniGrp.SelectedValue = row["id_unigrp"];
                chbLimitTovar.Checked = (bool)row["skoroportovar"];
                chbReglam.Checked = (bool)row["specification"];
                
                rbNetto.Checked = (int)row["id_unit"] == 1;                
                rbUnit.Checked = (int)row["id_unit"] == 2;

                tbDays.Text = row["DayMax"].ToString();
                tbUnit.Text = row["NettoMax"].ToString();
                if ((int)row["id_unit"] == 2)
                {
                    tbUnit.Text = ((decimal)row["NettoMax"]).ToString("0");
                }
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
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lDeps.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeps.Focus();
                return;
            }

            if (cmbUniGrp.SelectedValue == null)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lUniGrp.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUniGrp.Focus();
                return;
            }

            if (!rbNetto.Checked && !rbUnit.Checked)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lTypeUnit.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                return;
            }

            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lName.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbName.Focus();
                return;
            }

            decimal unit;

            if (tbUnit.Text.Trim().Length == 0 || !decimal.TryParse(tbUnit.Text, out unit))
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lNetto.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbUnit.Focus();
                return;
            }

            if (rbUnit.Checked)
            {

                if ((unit > 0 && unit < 1) || (unit != 0 && Math.Round(unit % (int)unit, 3) != 0))
                {
                    MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lNetto.Text}\"\nбез дробной части.\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int day;

            if (tbDays.Text.Trim().Length == 0 || !int.TryParse(tbDays.Text, out day))
            {
                MessageBox.Show(Config.centralText($"Необходимо заполнить\n \"{lDay.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDays.Focus();
                return;
            }

            string cName = tbName.Text.Trim();
            int id_otdel = (int)cmbDeps.SelectedValue;
            int id_unigrp = (int)cmbUniGrp.SelectedValue;
            int id_unit = rbNetto.Checked ? 1 : 2;
            bool specification = chbReglam.Checked;
            bool skoroportovar = chbLimitTovar.Checked;
            decimal NettoMax = unit;
            int DayMax = day;
            bool isActive = true;
            bool isDel = false;
            int result = 0;
            bool isAutoIncriments = false;

            Task<DataTable> task = Config.hCntMain.setGrp2(id, cName, id_otdel, id_unigrp, id_unit, specification, skoroportovar, NettoMax, DayMax, isActive, isDel, result, isAutoIncriments);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("В справочнике уже присутствует инвентаризационная группа с таким наименованием.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая ***.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            id = (int)dtResult.Rows[0]["id"];

            isAutoIncriments = true;
            task = Config.hCntSecond.setGrp2(id, cName, id_otdel, id_unigrp, id_unit, specification, skoroportovar, NettoMax, DayMax, isActive, isDel, result, isAutoIncriments);
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

        private void tbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rbUnit.Checked)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
            }
            else
            {
                if (e.KeyChar == '.')
                {
                    e.KeyChar = ',';
                }

                if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
                {
                    e.Handled = true;
                }
                else
                    if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
                {
                    if (e.KeyChar != '\b')
                    { e.Handled = true; }
                }
            }
        }

        private void tbDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != '\b';
        }

        private void cmbUniGrp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void chbReglam_CheckedChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void chbLimitTovar_CheckedChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void rbUnit_CheckedChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void rbNetto_CheckedChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void tbUnit_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void tbDays_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }

        private void frmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }
    }
}

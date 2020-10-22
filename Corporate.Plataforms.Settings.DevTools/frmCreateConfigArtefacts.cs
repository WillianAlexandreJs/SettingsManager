using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corporate.Plataforms.Settings.DevTools
{
    public partial class FrmCreateConfigArtefacts : Form
    {
        public FrmCreateConfigArtefacts()
        {
            InitializeComponent();
            InicializeDataGridSettings();
        }

        private void InicializeDataGridSettings()
        {
            DataGridViewComboBoxColumn referenceTypeColumn = dgvSettings.Columns["ReferenceType"] as DataGridViewComboBoxColumn;
            referenceTypeColumn.DataSource = Enum.GetValues(typeof(EReferenceType));
            referenceTypeColumn.ValueType = typeof(EReferenceType);

        }

        SettingsFileTool settingsFileTool;

        private void BtnReadFiles_Click(object sender, EventArgs e)
        {
            settingsFileTool = new SettingsFileTool(txtApplicationName.Text, txtPathConfigFile.Text, cbExtension.SelectedItem.ToString());

            foreach (var property in settingsFileTool.GetProperties())
            {
                int newRow = dgvSettings.Rows.Add();
                dgvSettings.Rows[newRow].Cells["Instance"].Value = property.Instance;
                dgvSettings.Rows[newRow].Cells["ReferenceType"].Value = EReferenceType.Setting;
                dgvSettings.Rows[newRow].Cells["Reference"].Value = property.Reference;
                dgvSettings.Rows[newRow].Cells["Property"].Value = property.Property;
                dgvSettings.Rows[newRow].Cells["Type"].Value = property.Type;
                dgvSettings.Rows[newRow].Cells["Value"].Value = property.Value;
                dgvSettings.Rows[newRow].HeaderCell.Value = string.Format("{0}", newRow + 1);
            }

            dgvSettings.Refresh();
            dgvSettings.Update();

        }

        private void BtnClasses_Click(object sender, EventArgs e)
        {
            settingsFileTool.CreateClassesFiles();
        }

        private void BtnInserts_Click(object sender, EventArgs e)
        {
            settingsFileTool.CreateInsertCommands();
        }

        public EReferenceType GetEnumByName(string descricao)
        {
            switch (descricao)
            {
                case "Setting":
                    return EReferenceType.Setting;
                case "Library":
                    return EReferenceType.Library;
                case "Application":
                    return EReferenceType.Application;
                default:
                    return EReferenceType.Setting;
            }
        }

        private void DgvSettings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSettings.IsCurrentCellDirty)
            {
                dgvSettings.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgvSettings.EndEdit();
            }
        }

        private void DgvSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).CurrentCell;

            string property = dgvSettings.Rows[cell.RowIndex].Cells["Property"].Value.ToString();
            string reference = dgvSettings.Rows[cell.RowIndex].Cells["Reference"].Value.ToString();
            EReferenceType referenceType = GetEnumByName(dgvSettings.Rows[cell.RowIndex].Cells["ReferenceType"].Value.ToString());

            foreach (DataGridViewRow row in dgvSettings.Rows)
            {
                if (row.Cells["Property"].Value.ToString() == property && row.Cells["Reference"].Value.ToString() == reference)
                    row.Cells["ReferenceType"].Value = referenceType;
            }

            settingsFileTool.UpdateProperties(property, reference, referenceType);

            dgvSettings.Refresh();
            dgvSettings.Update();
        }

    }
}

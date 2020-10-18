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
    public partial class frmCreateConfigArtefacts : Form
    {
        public frmCreateConfigArtefacts()
        {
            InitializeComponent();
        }

        SettingsFileTool configFileUtil;

        private void btnReadFiles_Click(object sender, EventArgs e)
        {
            configFileUtil = new SettingsFileTool(txtApplicationName.Text, txtPathConfigFile.Text, cbExtension.SelectedItem.ToString());
            
            foreach (var property in configFileUtil.GetProperties())
            {
                dgvProperties.Rows.Add(property.Instance, property.Reference, property.Property, property.Type, property.Value);
            }
            
            dgvProperties.Refresh();
            dgvProperties.Update();
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            configFileUtil.CreateClassesFiles();
        }

        private void btnInserts_Click(object sender, EventArgs e)
        {
            configFileUtil.CreateInsertCommands();
        }
    }
}

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
    public partial class FrmDevTools : Form
    {
        public FrmDevTools()
        {
            InitializeComponent();
        }

        private void HubTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestHubConnection frmTestHubConnection = new frmTestHubConnection
            {
                MdiParent = this
            };
            frmTestHubConnection.Show();
        }

        private void CreateAtfactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCreateConfigArtefacts frmCreateConfigArtefacts = new FrmCreateConfigArtefacts
            {
                MdiParent = this
            };
            frmCreateConfigArtefacts.Show();
        }
    }
}

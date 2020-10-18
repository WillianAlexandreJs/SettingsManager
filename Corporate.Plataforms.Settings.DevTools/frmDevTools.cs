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
    public partial class frmDevTools : Form
    {
        public frmDevTools()
        {
            InitializeComponent();
        }

        private void hubTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestHubConnection frmTestHubConnection = new frmTestHubConnection();
            frmTestHubConnection.MdiParent = this;
            frmTestHubConnection.Show();
        }

        private void createAtfactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateConfigArtefacts frmCreateConfigArtefacts = new frmCreateConfigArtefacts();
            frmCreateConfigArtefacts.MdiParent = this;
            frmCreateConfigArtefacts.Show();
        }
    }
}

using Corporate.Plataforms.Settings.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corporate.Plataforms.Settings.DevTools
{
    public partial class frmTestHubConnection : Form
    {
        public frmTestHubConnection()
        {
            InitializeComponent();
        }

        private void btnStartHub_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= txtInstancesNumber.Value; i++)
            {
                int instance = i;
                var runningTask = Task.Factory.StartNew(() => StartHub(instance, $"{txtApplicationName.Text}_{instance}"));
            }
        }

        private void StartHub(int instance, string clientId)
        {
            ClassTestConfiguration configuration = new ClassTestConfiguration();
            ApplicationConfigHub<ClassTestConfiguration> applicationConfigHub = new ApplicationConfigHub<ClassTestConfiguration>(configuration);
            applicationConfigHub.StartHubConnection(txtUrlHubConnection.Text, txtHubProxyName.Text, txtApplicationName.Text, clientId, new TimeSpan(0, 0, 5), UpdateSettingApplication =>
              {
                  Console.WriteLine($"{instance} -, {clientId}, Date: { DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}, Item: { JsonConvert.SerializeObject(configuration)}");
                  dgvUpdates.Rows.Add(instance.ToString(), clientId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), JsonConvert.SerializeObject(configuration));

              });
        }



    }
}

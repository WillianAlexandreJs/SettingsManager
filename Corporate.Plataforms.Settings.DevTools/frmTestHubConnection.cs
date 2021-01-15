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

        private void BtnStartHub_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= txtInstancesNumber.Value; i++)
            {
                int instance = i;
                var runningTask = Task.Factory.StartNew(() => StartHub($"{txtApplicationName.Text}_{instance}"));
            }
        }

        private void StartHub(string instanceId)
        {
            //DevToolsSettingsTestClass settingsTestClass = new DevToolsSettingsTestClass();
            //ApplicationConfigHub<DevToolsSettingsTestClass> applicationConfigHub = new ApplicationConfigHub<DevToolsSettingsTestClass>(settingsTestClass);
            //applicationConfigHub.StartHubConnection(txtUrlHubConnection.Text, txtHubProxyName.Text, txtApplicationName.Text, instanceId, new TimeSpan(0, 0, 5), 
            //    UpdateSettingApplication =>
            //  {
            //      Console.WriteLine($"{instanceId}, Date: { DateTime.Now:dd/MM/yyyy HH:mm:ss}, Item: { JsonConvert.SerializeObject(settingsTestClass)}");
            //      dgvUpdates.Rows.Add(instanceId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), JsonConvert.SerializeObject(settingsTestClass));

            //  },
            //  InitSettingApplication =>
            //  {
            //      Console.WriteLine($"{instanceId}, Date: { DateTime.Now:dd/MM/yyyy HH:mm:ss}, Item: { JsonConvert.SerializeObject(settingsTestClass)}");
            //      dgvUpdates.Rows.Add(instanceId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), JsonConvert.SerializeObject(settingsTestClass));
              
            //  });
        }



    }
}

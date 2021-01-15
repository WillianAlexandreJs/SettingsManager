using Corporate.Plataforms.Settings.Client;
using Corporate.Plataforms.Settings.Model;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Corporate.Plataforms.Settings.DevTools
{
    internal class SettingManager : ApplicationConfigHub<DevToolsSettingsTestClass>
    {

        /// <summary>
        /// Instância singleton desta classe
        /// </summary>
        private static SettingManager _instance = null;
        private System.Windows.Forms.DataGridView _dgvUpdates = null;
        private string ApplicationInstance;

        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static SettingManager Instance
        {
            get
            {
                return SettingManager._instance ?? (SettingManager._instance = new SettingManager());
            }
        }

        #endregion


        public void StartConnectionSettingsManager(string UrlSettingsManagerSignaR, string hub, string applicationName, string applicationInstance, ref System.Windows.Forms.DataGridView dgvUpdates)
        {
            _dgvUpdates = dgvUpdates;
            ApplicationInstance = applicationInstance;
            Console.WriteLine("Load Settings");
            Configuration = DevToolsSettingsTestClass.Instance;
            StartHubConnection(UrlSettingsManagerSignaR, hub, applicationName, applicationInstance, new TimeSpan(0, 0, 5));
            Console.WriteLine("Finish Load Settings");
        }

        protected override void UpdateSettingApplication(PropertyData itemConfig)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(itemConfig.SettingReference))
                {
                    object nestedProperty = Configuration.GetType().GetProperty(itemConfig.SettingReference).GetValue(Configuration);

                    if (nestedProperty == null)
                    {
                        Console.WriteLine($"Referencia {itemConfig.PropertyName} não encontrada");
                        return;
                    }

                    UpdatePropertyValue(nestedProperty, itemConfig);
                }
                else
                {
                    UpdatePropertyValue(Configuration, itemConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private new void UpdatePropertyValue(object obj, PropertyData itemConfig)
        {
            PropertyInfo propertyInfo;
            propertyInfo = obj.GetType().GetProperty(itemConfig.PropertyName);

            if (propertyInfo == null)
            {
                Console.WriteLine($"Propriedade {itemConfig.PropertyName} não encontrada");
                return;
            }

            if (propertyInfo.GetValue(obj)?.ToString() != itemConfig.PropertyValue)
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(itemConfig.PropertyValue, propertyInfo.PropertyType), null);
                Console.WriteLine($"Propriedade {propertyInfo.Name} atualizada com valor {propertyInfo.GetValue(obj)}");
                Console.WriteLine($"{ApplicationInstance}, Date: { DateTime.Now:dd/MM/yyyy HH:mm:ss}, Item: { JsonConvert.SerializeObject(itemConfig)}");
                _dgvUpdates.Rows.Add(ApplicationInstance, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), JsonConvert.SerializeObject(itemConfig));
            }

        }
    }
}





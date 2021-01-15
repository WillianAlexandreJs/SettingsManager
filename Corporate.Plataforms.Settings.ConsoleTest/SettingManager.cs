using Corporate.Plataforms.Settings.Client;
using Corporate.Plataforms.Settings.Model;
using System;
using System.Configuration;
using System.Reflection;

namespace Corporate.Plataforms.Settings.ConsoleTest
{

    internal class SettingManager : ApplicationConfigHub<ApplicationSettings>
    {

        /// <summary>
        /// Instância singleton desta classe
        /// </summary>
        private static SettingManager _instance = null;

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


        public void StartConnectionSettingsManager(string applicationName, string applicationInstance)
        {

            Console.WriteLine("Load Settings");
            Configuration = ApplicationSettings.Instance;
            StartHubConnection(ConfigurationManager.AppSettings.Get("UrlSettingsManagerSignaR"), "SettingsHub", applicationName, applicationInstance, new TimeSpan(0, 0, 5));
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
            }

        }
    }
}



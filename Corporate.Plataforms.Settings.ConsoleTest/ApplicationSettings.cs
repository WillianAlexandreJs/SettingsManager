using Corporate.Plataforms.Settings.Model;
using System;
using System.Reflection;

namespace ConsoleApp1
{ 
    public class ApplicationSettings
    {
        /// <summary>
        /// Instância singleton desta classe
        /// </summary>
        private static ApplicationSettings _instance = null;

        internal static ApplicationSettings Instance
        {
            get
            {
                return ApplicationSettings._instance ?? (ApplicationSettings._instance = new ApplicationSettings());
            }
        }

        public string CpuUsageCritical { get; set; }
        public string KeepAliveInterval { get; set; }
        public int FirstIntProperty { get; set; }
        public int SecondIntProperty { get; set; }
    }
}

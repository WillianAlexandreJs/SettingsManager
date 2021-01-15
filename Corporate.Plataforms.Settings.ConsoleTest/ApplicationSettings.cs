using Corporate.Plataforms.Settings.Model;
using System;
using System.Reflection;

namespace Corporate.Plataforms.Settings.ConsoleTest
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

        public ApplicationSettings()
        {
            ChildApplicationSettings = new ChildApplicationSettings();
        }

        public string FirstStringProperty { get; set; }
        public string SecondStringProperty { get; set; }
        public int FirstIntProperty { get; set; }
        public int SecondIntProperty { get; set; }
        public ChildApplicationSettings ChildApplicationSettings { get; set; }
    }

    public class ChildApplicationSettings
    {

        public string ChildFirstStringProperty { get; set; }
        public string ChildSecondStringProperty { get; set; }
        public int ChildFirstIntProperty { get; set; }
        public int ChildSecondIntProperty { get; set; }
    }
}

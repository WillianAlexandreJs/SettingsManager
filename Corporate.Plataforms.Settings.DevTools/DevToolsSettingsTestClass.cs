namespace Corporate.Plataforms.Settings.DevTools
{
    public class DevToolsSettingsTestClass
    {
        /// <summary>
        /// Instância singleton desta classe
        /// </summary>
        private static DevToolsSettingsTestClass _instance = null;

        internal static DevToolsSettingsTestClass Instance
        {
            get
            {
                return DevToolsSettingsTestClass._instance ?? (DevToolsSettingsTestClass._instance = new DevToolsSettingsTestClass());
            }
        }

        public string InstanceName { get; set; }

        public string Url { get; set; }

        public string SqlConnection { get; set; }

    }
}

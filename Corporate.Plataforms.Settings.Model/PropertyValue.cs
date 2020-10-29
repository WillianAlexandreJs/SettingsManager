using System;

namespace Corporate.Plataforms.Settings.Model
{
    public class PropertyValue
    {

        /// <summary>
        /// Property Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Setting Reference
        /// </summary>
        public string SettingReference { get; set; }

        /// <summary>
        /// Property Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Property Value
        /// </summary>
        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Entities
{
    public class IntegrationEntity
    {
        /// <summary>
        /// Instance Name
        /// </summary>
        [Column("INSTANCE_NAME")]
        public string InstanceName { get; set; }

        /// <summary>
        /// Property Name
        /// </summary>
        [Column("PROPERTY_NAME")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Setting Reference
        /// </summary>
        [Column("SETTING_REFERENCE")]
        public string SettingReference { get; set; }

        /// <summary>
        /// Property Type
        /// </summary>
        [Column("PROPERTY_TYPE")]
        public string PropertyType { get; set; }


        /// <summary>
        /// Property Value
        /// </summary>
        [Column("PROPERTY_VALUE")]
        public string PropertyValue { get; set; }


    }
}

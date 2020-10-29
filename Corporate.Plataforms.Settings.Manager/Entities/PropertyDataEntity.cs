using AspNetCore.EntityFramework.Procedure.Tools;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corporate.Plataforms.Settings.Manager.Entities
{
    public class PropertyDataEntity : IEntity
    {
        
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

        /// <summary>
        /// Instance Name
        /// </summary>
        [Column("INSTANCE_NAME")]
        public string InstanceName { get; set; }



    }
}

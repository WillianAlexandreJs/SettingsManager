using AspNetCore.EntityFramework.Procedure.Tools;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corporate.Plataforms.Settings.Manager.Entities
{
    public class InstanceEntity : IEntity
    {
        /// <summary>
        /// Instance Id
        /// </summary>
        [Key]
        [Column("INSTANCE_ID")]
        public int InstanceId { get; set; }

        /// <summary>
        /// Instance Name
        /// </summary>
        [Column("INSTANCE_NAME")]
        public string InstanceName { get; set; }

        /// <summary>
        /// Application Id
        /// </summary>
        [Column("APPLICATION_ID")]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Application Name
        /// </summary>
        [Column("APPLICATION_NAME")]
        public string ApplicationName { get; set; }

    }
}

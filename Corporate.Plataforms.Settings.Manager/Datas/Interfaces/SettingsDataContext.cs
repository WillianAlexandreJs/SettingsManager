using AspNetCore.EntityFramework.Procedure.Tools;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace Corporate.Plataforms.Settings.Manager.Datas.Interfaces
{
    public class SettingsDataContext : AContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">DbContextOptions<SettingsDataContext></param>
        public SettingsDataContext(DbContextOptions<SettingsDataContext> options) : base(options)
        {
        }

        /// <summary>
        /// Map Keys
        /// </summary>
        /// <param name="modelBuilder">model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyDataEntity>().HasKey(x => new { x.PropertyName, x.SettingReference, x.InstanceName });
        }

        /// <summary>
        /// DbSet PropertyData
        /// </summary>
        public virtual DbSet<PropertyDataEntity> GetPropertiesData { get; set; }

        /// <summary>
        /// DbSet PropertyData
        /// </summary>
        public virtual DbSet<InstanceEntity> GetInstances { get; set; }
    }
}

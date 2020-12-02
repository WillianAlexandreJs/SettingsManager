using Microsoft.EntityFrameworkCore;

namespace AspNetCore.EntityFramework.Procedure.Tools
{
    /// <summary>
    /// Abstract class to Sql Server context
    /// </summary>
    public abstract class AContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        public AContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// DbSet transactions return
        /// </summary>
        public virtual DbSet<TransactionCommandResult> GetTransactionCommandResult { get; set; }
    }
}

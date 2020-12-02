using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.EntityFramework.Procedure.Tools
{

    /// <summary>
    /// Transaction result properties
    /// </summary>
    public class TransactionCommandResult
    {
        /// <summary>
        /// Id
        /// </summary>
        [NotMapped]
        public object Output { get; set; }

        /// <summary>
        /// Rows affected
        /// </summary>
        [Key]
        [Column("RowsAffected")]
        public int RowsAffected { get; set; }

        /// <summary>
        /// Error Number
        /// </summary>
        [Column("ErrorNumber")]
        public int? ErrorNumber { get; set; }

        /// <summary>
        /// Stored Procedure Name
        /// </summary>
        [Column("StoredProcedure")]
        public string StoredProcedure { get; set; }

        /// <summary>
        /// Line Number
        /// </summary>
        [Column("ErrorLineNumber")]
        public int? ErrorLineNumber { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        [Column("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}

namespace AspNetCore.EntityFramework.Procedure.Tools
{
    /// <summary>
    /// Search results pagination properties
    /// </summary>
    public class PaginationReturn
    {
        /// <summary>
        /// Page Number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number records per page
        /// </summary>
        public int RecordsPage { get; set; }

        /// <summary>
        /// Number amount records
        /// </summary>
        public int AmountRecords { get; set; }

    }

    /// <summary>
    /// Pagination data
    /// </summary>
    public class PaginationReturn<TData> : PaginationReturn
    {
        /// <summary>
        /// Type Generic
        /// </summary>
        public TData Data { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AspNetCore.EntityFramework.Procedure.Tools
{
    /// <summary>
    ///  Abstract class to Sql Server Repository
    /// </summary>
    /// <typeparam name="TContext">IContext</typeparam>
    public abstract class ARepository<TContext> where TContext : AContext
    {
        #region Properties

        protected readonly TContext _context;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">database context</param>
        public ARepository(TContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Execute Transaction Command
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">SqlParameters</param>
        /// <returns>Result Transaction</returns>
        protected TransactionCommandResult ExecuteTransactCommand(string query, SqlParameter[] parameters)
        {
            try
            {
                TransactionCommandResult transactionreturn = _context.GetTransactionCommandResult.FromSql(query, parameters).FirstOrDefault();

                if (transactionreturn.ErrorNumber > 0)
                    throw new Exception($"Procedure: {transactionreturn.StoredProcedure} - Line: {transactionreturn.ErrorLineNumber} - ErrorNumber: {transactionreturn.ErrorNumber} - {transactionreturn.ErrorMessage}");

                return transactionreturn;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error execute Sql command. \n\nDetails: {ex.Message}\n{ex.InnerException}");
            }
        }


        /// <summary>
        /// Execute Transaction Command with OutPut Parameter
        /// </summary>
        /// <param name="output">Output Parameter</param>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">SqlParameters</param>
        /// <returns>Result Transaction</returns>
        protected TransactionCommandResult ExecuteTransactCommandWithParameterOutput<T>(out T output, string query, params SqlParameter[] parameters) where T: struct
        {
            try
            {
                var outputParameter = parameters.FirstOrDefault(x => x.Direction == ParameterDirection.Output);

                TransactionCommandResult transactionreturn = _context.GetTransactionCommandResult.FromSql(query, parameters).FirstOrDefault();

                if (transactionreturn.ErrorNumber > 0)
                    throw new Exception($"Procedure: {transactionreturn.StoredProcedure} - Line: {transactionreturn.ErrorLineNumber} - ErrorNumber: {transactionreturn.ErrorNumber} - {transactionreturn.ErrorMessage}");

                output = (T)outputParameter.Value;

                return transactionreturn;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error execute Sql command. \n\nDetails: {ex.Message}\n{ex.InnerException}");
            }
        }

        /// <summary>
        /// Método responsável por listar os dados de uma Entity
        /// </summary>
        /// <typeparam name="Entity">Entity</typeparam>
        /// <param name="dbSet">DbSet da Entity</param>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">parameters</param>
        /// <returns>Entity/returns>
        protected List<Entity> ExecuteQuery<Entity>(DbSet<Entity> dbSet, string query, params SqlParameter[] parameters) where Entity : class, IEntity
        {
            try
            {
                return dbSet.FromSql(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching {typeof(Entity).Name}(s).\n\nDetails: {ex.Message}\n{ex.InnerException}");
            }
        }

        /// <summary>
        /// Método responsável por buscar os dados de uma Entity
        /// </summary>
        /// <typeparam name="Entity">Entity</typeparam>
        /// <param name="dbSet">DbSet da Entity</param>
        /// <param name="query">Query String</param>
        /// <param name="parameters">parameters</param>
        /// <returns>Entity/returns>
        protected List<Entity> ExecuteQueryUpdateContext<Entity>(DbSet<Entity> dbSet, string query, params SqlParameter[] parameters) where Entity : class, IEntity
        {
            try
            {
                return dbSet.AsNoTracking().FromSql(query, parameters).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching {typeof(Entity).Name}.\n\nDetails: {ex.Message}\n{ex.InnerException}");
            }
        }

        /// <summary>
        /// Método responsável por listar os dados de uma Entity
        /// </summary>
        /// <typeparam name="Entity">Entity</typeparam>
        /// <param name="dbSet">DbSet da Entity</param>
        /// <param name="query">Query String</param>
        /// <param name="parameters">parameters</param>
        /// <returns>Entity/returns>
        protected PaginationReturn<List<Entity>> ExecuteQueryPagination<Entity>(int? page, int? recordsPerPage, DbSet<Entity> dbSet, string query, params SqlParameter[] parameters) where Entity : class, IEntity
        {
            try
            {
                PaginationReturn<List<Entity>> paginacao = new PaginationReturn<List<Entity>>();

                var amountRecordsParameter = parameters.FirstOrDefault(x => x.Direction == ParameterDirection.Output);

                paginacao.Data = dbSet.FromSql(query, parameters).ToList();

                paginacao.AmountRecords = amountRecordsParameter.Value == null ? 1 : (int)amountRecordsParameter?.Value;
                paginacao.PageNumber = page ?? 1;
                paginacao.RecordsPage = recordsPerPage ?? 1;

                return paginacao;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching page {typeof(Entity).Name}.\n\nDetails: {ex.Message}\n{ex.InnerException}");
            }

        }
        
        #endregion Methods

    }
}

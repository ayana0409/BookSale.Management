using Dapper;
using System.Data;

namespace BookSale.Management.Doman
{
    public interface ISQLQueryHandler
    {
        Task ExecuteNonReturn(string query, DynamicParameters parameters = null, IDbTransaction transaction = null);
        Task<T?> ExecuteReturnSingleRowScalarAsync<T>(string query, DynamicParameters parameters = null, IDbTransaction transaction = null);
        Task<T?> ExecuteReturnSingleValueScalarAsync<T>(string query, DynamicParameters parameters = null, IDbTransaction transaction = null);
        Task<IEnumerable<T>> ExecuteStoreProdecureReturnListAsync<T>(string storeName, DynamicParameters parameters = null, IDbTransaction transaction = null);
    }
}
using BookSale.Management.Doman;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookSale.Management.DataAccess.Dapper
{
    public class SQLQueryHandler : ISQLQueryHandler
    {
        private readonly string _connectionString = string.Empty;

        public SQLQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task ExecuteNonReturn(string query, DynamicParameters parameters, IDbTransaction transaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param: parameters, transaction);
            }

        }

        public async Task<T?> ExecuteReturnSingleValueScalarAsync<T>(string query, DynamicParameters parameters, IDbTransaction transaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<T>(query, param: parameters, transaction);
            }

        }

        public async Task<T?> ExecuteReturnSingleRowScalarAsync<T>(string query, DynamicParameters parameters, IDbTransaction transaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleAsync<T>(query, param: parameters, transaction);
            }

        }


        public async Task<IEnumerable<T>> ExecuteStoreProdecureReturnListAsync<T>(string storeName, DynamicParameters parameters, IDbTransaction transaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(storeName, param: parameters, transaction, commandType: CommandType.StoredProcedure);
            }

        }

    }
}

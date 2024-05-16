using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Doman;
using Dapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ISQLQueryHandler _sQLQueryHandler;

        public OrderRepository(ApplicationDbContext applicationDbContext, ISQLQueryHandler sQLQueryHandler) : base(applicationDbContext)
        {
            _sQLQueryHandler = sQLQueryHandler;
        }

        public async Task SaveAsync(Order order)
        {
            await base.Create(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await GetAllAsync();
        }

        public async Task<(IEnumerable<T>, int)> GetByPagination<T>(int pageIndex, int pageSize, string keyword)
        {
            DynamicParameters parameters = new();

            parameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("pageIndex", pageIndex, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("totalRecords", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var result = await _sQLQueryHandler.ExecuteStoreProdecureReturnListAsync<T>("spGetAllOrderByPagination", parameters);

            var totalRecord = parameters.Get<int>("totalRecords");

            return (result, totalRecord);
        }
    }
}

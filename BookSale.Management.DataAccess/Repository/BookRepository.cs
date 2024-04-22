using BookSale.Managament.Domain.Abtract;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Doman;
using Dapper;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.DataAccess.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISQLQueryHandler _sQLQueryHandler;

        public BookRepository(ApplicationDbContext applicationDbContext, ISQLQueryHandler sQLQueryHandler) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _sQLQueryHandler = sQLQueryHandler;
        }

        public async Task<(IEnumerable<BookDTO>, int)> GetBookByPanigation<BookDTO>(int pageIndex, int pageSize, string keyword)
        {
            DynamicParameters parameters = new();

            parameters.Add("keyword", "", System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("pageIndex", pageIndex, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("totalRecords", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            var result = await _sQLQueryHandler.ExecuteStoreProdecureReturnListAsync<BookDTO>("GetBookByPagination", parameters);

            var totalRecord = parameters.Get<int>("totalRecords"); 

            return (result, totalRecord);
        }
    }
}

using BookSale.Managament.Domain.Abtract;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Doman;
using Dapper;

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

        public async Task<(IEnumerable<BookDTO>, int)> GetBookByPanigationAsync<BookDTO>(int pageIndex, int pageSize, string? keyword)
        {
            DynamicParameters parameters = new();

            parameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("pageIndex", pageIndex, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameters.Add("totalRecords", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var result = await _sQLQueryHandler.ExecuteStoreProdecureReturnListAsync<BookDTO>("GetBookByPagination", parameters);

            var totalRecord = parameters.Get<int>("totalRecords");

            return (result, totalRecord);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await base.GetSigleAsync(x => x.Id == id);
        }

        public async Task<Book?> GetByCodeAsync(string code) => await base.GetSigleAsync(x => x.Code == code);
        public async Task<IEnumerable<Book?>> GetBookByListCodeAsync(string[] codes) => await base.GetAllAsync(x => codes.Contains(x.Code));

        public async Task<bool> SaveAsync(Book book)
        {
            try
            {
                if (book.Id == 0)
                {
                    await base.CreateAsync(book);
                }
                else
                {
                    base.Update(book);
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<(IEnumerable<Book>, int)> GetBookForSiteAsync(int genreId, int pageIndex, int pageSize = 10)
        {
            IEnumerable<Book> books;

            books = await base.GetAllAsync(x => (genreId == 0 || x.GenreId == genreId) && x.IsActive);

            var totalRecord = books.Count();

            books = books.Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize * pageIndex).ToList();

            return (books, totalRecord);
        }
    }
}

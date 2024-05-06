using BookSale.Managament.Domain.Abtract;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Doman;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISQLQueryHandler _sQLQueryHandler;
        private IGenreRepository? _genreRepository;
        private IBookRepository? _bookRepository;
        public UnitOfWork(ApplicationDbContext applicationDbContext, ISQLQueryHandler sQLQueryHandler)
        {
            _applicationDbContext = applicationDbContext;
            _sQLQueryHandler = sQLQueryHandler;
        }
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_applicationDbContext);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_applicationDbContext, _sQLQueryHandler);

        public async Task SaveChangeAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            if (_applicationDbContext != null)
            {
                _applicationDbContext.Dispose();
            }
        }
    }
}

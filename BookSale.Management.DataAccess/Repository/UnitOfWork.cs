using BookSale.Managament.Domain.Abtract;
using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Doman;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISQLQueryHandler _sQLQueryHandler;
        private IGenreRepository? _genreRepository;
        private IBookRepository? _bookRepository;
        private IUserAddressRepository? _userAddressRepository;
        private ICartRepository? _cartRepository;
        private IOrderRepository? _orderRepository;
        private IDbContextTransaction _dbContextTransaction;
        public UnitOfWork(ApplicationDbContext applicationDbContext, ISQLQueryHandler sQLQueryHandler)
        {
            _applicationDbContext = applicationDbContext;
            _sQLQueryHandler = sQLQueryHandler;
        }
        public DbSet<T> Table<T>() where T : class => _applicationDbContext.Set<T>();

        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_applicationDbContext);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_applicationDbContext, _sQLQueryHandler);
        public IUserAddressRepository UserAddressRepository => _userAddressRepository ??= new UserAddressRepository(_applicationDbContext);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_applicationDbContext);
        public ICartRepository CartRepository => _cartRepository ??= new CartRepository(_applicationDbContext);

        public async Task BeginTransaction()
        {
            _dbContextTransaction = await _applicationDbContext.Database.BeginTransactionAsync();
        }
        
        public async Task CommitTransaction()
        {
            await _dbContextTransaction?.CommitAsync();
        }

        public async Task RollBackTransaction()
        {
            await _dbContextTransaction?.RollbackAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _applicationDbContext?.SaveChangesAsync();
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

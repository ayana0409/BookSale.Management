using BookSale.Managament.Domain.Abtract;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }
        IUserAddressRepository UserAddressRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICartRepository CartRepository { get; }

        Task BeginTransaction();
        Task CommitTransaction();
        Task RollBackTransaction();
        Task SaveChangeAsync();
        DbSet<T> Table<T>() where T : class;
    }
}
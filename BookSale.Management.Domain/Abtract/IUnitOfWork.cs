using BookSale.Managament.Domain.Abtract;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }
        IUserAddressRepository UserAddressRepository { get; }

        Task SaveChangeAsync();
    }
}
using BookSale.Managament.Domain.Abtract;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }

        Task SaveChangeAsync();
    }
}
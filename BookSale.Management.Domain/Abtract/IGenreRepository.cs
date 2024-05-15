using BookSale.Managament.Domain.Entities;

namespace BookSale.Managament.Domain.Abtract
{
    public interface IGenreRepository
    {
        Task<bool> AddAsync(Genre genre);
        Task<Genre> FindById(int id);
        Task<IEnumerable<Genre>> GetAllActiveGenre();
        Task<IEnumerable<Genre>> GetAllGenre();
        Task<Genre> GetById(int id);
        Task<bool> UpdateAsync(Genre genre);
    }
}

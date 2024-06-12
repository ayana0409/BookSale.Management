using BookSale.Managament.Domain.Entities;

namespace BookSale.Managament.Domain.Abtract
{
    public interface IGenreRepository
    {
        Task<bool> AddAsync(Genre genre);
        Task<Genre> FindByIdAsync(int id);
        Task<IEnumerable<Genre>> GetAllActiveAsync();
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Genre genre);
    }
}

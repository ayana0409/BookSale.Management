using BookSale.Managament.Domain.Entities;

namespace BookSale.Managament.Domain.Abtract
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book?>> GetBookByListCodeAsync(string[] codes);
        Task<(IEnumerable<T>, int)> GetBookByPanigationAsync<T>(int pageIndex, int pageSize, string? keyword);
        Task<(IEnumerable<Book>, int)> GetBookForSiteAsync(int genreId, int pageIndex, int pageSize = 10);
        Task<Book?> GetByCodeAsync(string code);
        Task<Book?> GetByIdAsync(int id);
        Task<bool> SaveAsync(Book book);
    }
}

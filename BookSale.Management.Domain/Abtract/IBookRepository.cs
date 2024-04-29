using BookSale.Managament.Domain.Entities;

namespace BookSale.Managament.Domain.Abtract
{
    public interface IBookRepository
    {
        Task<(IEnumerable<T>, int)> GetBookByPanigation<T>(int pageIndex, int pageSize, string keyword);
        Task<(IEnumerable<Book>, int)> GetBookForSite(int genreId, int pageIndex, int pageSize = 10);
        Task<Book?> GetByCode(string code);
        Task<Book?> GetById(int id);
        Task<bool> Save(Book book);
    }
}

using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModal;

namespace BookSale.Management.Application.Abtracts
{
    public interface IBookService
    {
        Task<ResponseModel> DeleteAsync(int id);
        Task<string> GenerateCodeAsync(int number = 10);
        Task<ResponseDatatable<BookDTO>> GetBookByPagination(RequestDatatable request);
        Task<(IEnumerable<BookDTO>, int)> GetBookForSiteAsync(int genreId, int pageIndex, int pageSize = 10);
        Task<BookViewModal> GetById(int id);
        Task<ResponseModel> SaveAsync(BookViewModal bookVM);
    }
}
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.ViewModal;
using BookSale.Management.UI.Models;

namespace BookSale.Management.Application.Abtracts
{
    public interface IBookService
    {
        Task<ResponseModel> DeleteAsync(int id);
        Task<string> GenerateCodeAsync(int number = 10);
        Task<IEnumerable<BookCartDTO>> GetBookByListCodeAsync(string[] codes);
        Task<ResponseDatatable<BookDTO>> GetBookByPaginationAsync(RequestDatatable request);
        Task<BookForSiteDTO> GetBookForSiteAsync(int genreId, int pageIndex, int pageSize = 12);
        Task<BookViewModal> GetByIdAsync(int id);
        Task<ResponseModel> SaveAsync(BookViewModal bookVM);
    }
}
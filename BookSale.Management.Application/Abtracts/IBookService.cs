using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abtracts
{
    public interface IBookService
    {
        Task<ResponseDatatable<BookDTO>> GetBookByPagination(RequestDatatable request);
    }
}
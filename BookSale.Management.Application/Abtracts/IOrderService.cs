using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.Order;

namespace BookSale.Management.Application.Abtracts
{
    public interface IOrderService
    {
        Task<ResponseDatatable<object>> GetOrderByPagination(RequestDatatable request);
        Task<bool> Save(OrderRequestDTO order);
    }
}
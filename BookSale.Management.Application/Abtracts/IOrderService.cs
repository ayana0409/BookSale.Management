using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.Order;
using BookSale.Management.Application.DTOs.Report;

namespace BookSale.Management.Application.Abtracts
{
    public interface IOrderService
    {
        Task<ResponseDatatable<object>> GetOrderByPagination(RequestDatatable request);
        Task<ReportDTO> GetReportByIdAsync(string id);
        Task<IEnumerable<ReportOrderResponseDTO>> GetReportOrderAsync(ReportRequestDTO request);
        Task<bool> Save(OrderRequestDTO order);
    }
}
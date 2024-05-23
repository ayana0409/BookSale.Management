using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Order;
using BookSale.Management.DataAccess.Repository;
using BookSale.Managament.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using BookSale.Management.Application.DTOs.Report;
using BookSale.Management.Application.DTOs.Char;

namespace BookSale.Management.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDatatable<object>> GetOrderByPagination(RequestDatatable request)
        {
            var (orders, totalRecords) = await _unitOfWork.OrderRepository.GetByPagination<OrderResponseDTO>(request.SkipItems, request.PageSize, request.Keyword is null ? "" : request.Keyword);

            return new ResponseDatatable<object>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = orders.Select(x => new
                {
                    Id = x.Id,
                    Code = x.Code,
                    CreatedOn = x.CreatedOn,
                    FullName = x.FullName,
                    TotalPrice = x.TotalOrder,
                    Status = Enum.GetName(typeof(StatusProcessing), x.Status),
                    PaymentMethod = Enum.GetName(typeof(PaymentMethod), x.PaymentMethod),
                }).ToList()
            };
        }

        public async Task<bool> Save(OrderRequestDTO orderDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var order = _mapper.Map<Order>(orderDTO);


                await _unitOfWork.OrderRepository.SaveAsync(order);

                await _unitOfWork.SaveChangeAsync();

                if (orderDTO.Books.Count > 0 )
                {
                    foreach ( var book in orderDTO.Books )
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = book.Id,
                            Quantity = book.Quantity,
                            UnitPrice = book.Price
                        };

                        await _unitOfWork.Table<OrderDetail>().AddAsync(orderDetail);
                    }

                    await _unitOfWork.SaveChangeAsync();
                };

                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollBackTransaction();
                
                return false;
            }

            return true;
        }

        public async Task<ReportDTO> GetReportByIdAsync(string id)
        {
            var order = await _unitOfWork.Table<Order>()
                .Where(x => x.Id == id)
                .Include(x => x.Address)
                .Include(x => x.Details)
                .SingleAsync();

            var address = _mapper.Map<OrderAddressDTO>(order.Address);

            var details = order.Details.Join(_unitOfWork.Table<Book>(), 
                                            x => x.ProductId, 
                                            y => y.Id, 
                                            (details, book) => new OrderDetailDTO
                                            {
                                                UnitPrice = details.UnitPrice,
                                                ProductName = book.Title,
                                                Quantity = details.Quantity
                                            }).ToList();

            return new ReportDTO
            {
                code = order.Code,
                Address = address,
                CreateOn = order.CreatedOn,
                Details = details
            };
        }

        public async Task<IEnumerable<ReportOrderResponseDTO>> GetReportOrderAsync(ReportRequestDTO request)
        {
            var result = await _unitOfWork.OrderRepository.GetReportByExcel<ReportOrderResponseDTO>(request.From,
                                                                                                    request.To, 
                                                                                                    request.GenreId, 
                                                                                                    (int)request.Status);

            return result;
        }

        public async Task<IEnumerable<OrderChartByGenreDTO>> GetCharDataByGenreAsync(int genreId)
        {
            var result = await _unitOfWork.OrderRepository.GetChartDataByGenre<OrderChartByGenreDTO>(genreId);

            return result;
        }
    }
}

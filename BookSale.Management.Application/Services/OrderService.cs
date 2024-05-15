using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.Order;
using BookSale.Management.DataAccess.Repository;

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
                }

                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollBackTransaction();
                
                return false;
            }

            return true;
        }
    }
}

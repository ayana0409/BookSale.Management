using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.Cart;
using BookSale.Management.DataAccess.Repository;

namespace BookSale.Management.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SaveAsync(CartRequestDTO bookCartDTOs)
        {
            try
            {
                var cart = _mapper.Map<Cart>(bookCartDTOs);

                await _unitOfWork.BeginTransaction();

                await _unitOfWork.CartRepository.SaveAsync(cart);

                await _unitOfWork.SaveChangeAsync();

                if (bookCartDTOs.Books.Count > 0)
                {
                    foreach (var book in bookCartDTOs.Books)
                    {
                        var cartDetail = new CartDetail
                        {
                            CartId = cart.Id,
                            BookId = book.Id,
                            Quantity = book.Quantity,
                            Price = book.Price
                        };

                        await _unitOfWork.Table<CartDetail>().AddAsync(cartDetail);
                    }

                    await _unitOfWork.SaveChangeAsync();
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

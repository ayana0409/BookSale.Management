using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.Cart;

namespace BookSale.Management.Application.Abtracts
{
    public interface ICartService
    {
        Task<bool> Save(CartRequestDTO bookCartDTOs);
    }
}
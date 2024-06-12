using BookSale.Managament.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public interface ICartRepository
    {
        Task SaveAsync(Cart cart);
    }
}
using BookSale.Managament.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public interface ICartRepository
    {
        Task Save(Cart cart);
    }
}
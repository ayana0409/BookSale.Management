using BookSale.Managament.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
    }
}
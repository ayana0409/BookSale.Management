using BookSale.Managament.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<(IEnumerable<T>, int)> GetByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task SaveAsync(Order order);
    }
}
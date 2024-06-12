using BookSale.Managament.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public interface IUserAddressRepository
    {
        Task<IEnumerable<UserAddress>> GetAllAddressByUserAsync(string userId);
        Task SaveAsync(UserAddress userAddress);
    }
}
using BookSale.Management.Application.DTOs.User;

namespace BookSale.Management.Application.Abtracts
{
    public interface IUserAddressService
    {
        Task<IEnumerable<UserAddressDTO>> GetUserAddressForSite(string userId);
        Task<int> SaveAsync(UserAddressDTO userAddressDTO);
    }
}
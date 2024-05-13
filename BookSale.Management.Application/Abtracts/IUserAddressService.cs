using BookSale.Management.Application.DTOs.User;

namespace BookSale.Management.Application.Abtracts
{
    public interface IUserAddressService
    {
        Task<IEnumerable<UserAdressDTO>> GetUserAddressForSite(string userId);
    }
}
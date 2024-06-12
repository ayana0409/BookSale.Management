using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abtracts
{
    public interface IUserService
    {
        Task<bool> DeleteAsync(string id);
        Task<AccountDTO> GetUserByIdAsync(string id);
        Task<ResponseDatatable<UserModel>> GetUserByPaginationAsync(RequestDatatable request);
        Task<ResponseModel> SaveAsync(AccountDTO account);
    }
}
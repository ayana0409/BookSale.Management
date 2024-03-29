using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abtracts
{
    public interface IUserService
    {
        Task<AccountDTO> GetUserById(string id);
        Task<ResponseDatatable<UserModel>> GetUserByPagination(RequestDatatable request);
        Task<ResponseModel> Save(AccountDTO account);
    }
}
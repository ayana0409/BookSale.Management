using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Services
{
    public interface IUserService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember);
        Task<ResponseDatatable<UserModel>> GetUserByPagination(RequestDatatable request);
    }
}
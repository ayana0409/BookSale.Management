using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return new ResponseModel
                {
                    Message = "Username or password is invalid"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: hasRemember, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var remainingLockout = user.LockoutEnd.Value - DateTimeOffset.UtcNow;

                return new ResponseModel
                {
                    Message = $"Account is locked out. Please try again in {Math.Round(remainingLockout.TotalMinutes)} minutes"
                };
            }

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Message = "Username or password is invalid"
                };

            }

            if (user.AccessFailedCount > 0)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            return new ResponseModel
            {
                Status = true
            };
        }

        public async Task<ResponseDatatable<UserModel>> GetUserByPagination(RequestDatatable request)
        {
            var users = await _userManager.Users.Where(x => String.IsNullOrEmpty(request.Keyword)
                                                   || (x.UserName.Contains(request.Keyword)
                                                   || x.Email.Contains(request.Keyword)
                                                   || x.FullName.Contains(request.Keyword)
                                                   || x.PhoneNumber.Contains(request.Keyword))
                                                 )
                                          .Select(x => new UserModel {
                                                    ID = x.Id,
                                                    UserName = x.UserName,
                                                    Email = x.Email,
                                                    FullName = x.FullName,
                                                    Phone = x.PhoneNumber,
                                                    IsActived = x.IsActive ? "Yes" : "No"
                                          }).ToListAsync();

            int totalRecord = users.Count;

            var result = users.Skip(request.SkipItems).Take(request.PageSize).ToList();

            return new ResponseDatatable<UserModel> {
                                                    Draw = request.Draw,
                                                    RecordsTotal = totalRecord,
                                                    RecordsFiltered = totalRecord,
                                                    Data = result
                                                    };
        }
    }
}

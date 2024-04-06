using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IImageService imageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ResponseDatatable<UserModel>> GetUserByPagination(RequestDatatable request)
        {
            var users = await _userManager.Users.Where(x => x.IsActive && (String.IsNullOrEmpty(request.Keyword)
                                                   || (x.UserName.Contains(request.Keyword)
                                                   || x.Email.Contains(request.Keyword)
                                                   || x.FullName.Contains(request.Keyword)
                                                   || x.PhoneNumber.Contains(request.Keyword)))
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

        public async Task<AccountDTO> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var userDto = _mapper.Map<AccountDTO>(user);

            userDto.RoleName = role;

            return userDto;
        }

        public async Task<ResponseModel> Save(AccountDTO account)
        {
            string errors = string.Empty;
            IdentityResult identityResult;

            if (string.IsNullOrEmpty(account.Id))
            {
                var applicationUser = new ApplicationUser
                {
                    FullName = account.Fullname,
                    UserName = account.Username,
                    Email = account.Email,
                    IsActive = account.IsActive,
                    PhoneNumber = account.Phone,
                    MobilePhone = account.MobilePhone,
                    Address = account.Address
                };

                identityResult = await _userManager.CreateAsync(applicationUser, account.Password);

                if(identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, account.RoleName);

                    await _imageService.SaveImage(new List<IFormFile> { account.Avatar }, "images/user", $"{applicationUser.Id}.png");

                    return new ResponseModel
                    {
                        Action = Managament.Domain.Enums.ActionType.Insert,
                        Message = "Insert successful.",
                        Status = true,
                    };
                }
            }
            else
            {
                var user = await _userManager.FindByIdAsync(account.Id);

                user.FullName = account.Fullname;
                user.Email = account.Email;
                user.IsActive = account.IsActive;
                user.PhoneNumber = account.Phone;
                user.MobilePhone = account.MobilePhone;
                user.Address = account.Address;

                identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {
                    await _imageService.SaveImage(new List<IFormFile> { account.Avatar }, "images/user", $"{user.Id}.png");

                    var hasRole = await _userManager.IsInRoleAsync(user, account.RoleName);

                    if (!hasRole)
                    {
                        var oldRolesName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                        var removeResult = await _userManager.RemoveFromRoleAsync(user, oldRolesName);

                        if (removeResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, account.RoleName);
                        }
                    }

                    return new ResponseModel
                    {
                        Action = Managament.Domain.Enums.ActionType.Update,
                        Message = "Update successful.",
                        Status = true,
                    };
                }
            }
            errors = string.Join("<br/>", identityResult.Errors);

            return new ResponseModel
            {
                Action = Managament.Domain.Enums.ActionType.Insert,
                Message = $"{(string.IsNullOrEmpty(account.Id)? "Insert" : "Update")} failes. {errors}",
                Status = false,
            };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);

                return true;
            }
            return false;
        }
    }
}

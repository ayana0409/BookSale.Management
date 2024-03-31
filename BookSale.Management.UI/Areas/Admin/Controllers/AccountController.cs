using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
 
        public AccountController(IUserService userService, IRoleService roleService) 
        {
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAccountPagination(RequestDatatable requestDatatable)
        {
            var data = await _userService.GetUserByPagination(requestDatatable);

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> SaveData(string? id)
        {
            AccountDTO accountDto = !string.IsNullOrEmpty(id) ? await _userService.GetUserById(id) : new();

            ViewBag.Roles = await _roleService.GetRoleForDropdownList();

            return View(accountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(AccountDTO accountDTO)
        {
            if (!string.IsNullOrEmpty(accountDTO.Id) && string.IsNullOrEmpty(accountDTO.Password))
            {
                ModelState.Remove(nameof(accountDTO.Password)); // Loại bỏ password khỏi ModelState
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleService.GetRoleForDropdownList();
                ModelState.AddModelError("errorsModel", "Invalid model");

                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage).ToList().ToList();

                ViewBag.Error = string.Join("<br/>", errors);

                return View(accountDTO);
            }


            var result = await _userService.Save(accountDTO);

            if (result.Status)
            {
                return RedirectToAction("", "Account");
            }

            ViewBag.Roles = await _roleService.GetRoleForDropdownList();
            ModelState.AddModelError("errorsModel", result.Message);

            return View(accountDTO);
        }
    }
}

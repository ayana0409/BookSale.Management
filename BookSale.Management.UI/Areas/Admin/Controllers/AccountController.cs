using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) 
        {
            _userService = userService;
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
        public IActionResult SaveData(string id)
        {
            var accountDto = new AccountDTO();
            return View(accountDto);
        }

        [HttpPost]
        public IActionResult SaveData(AccountDTO accountDTO)
        {
            var acc = accountDTO;

            return View();
        }
    }
}

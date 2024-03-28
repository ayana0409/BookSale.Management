using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Services;
using BookSale.Management.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {

        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInUser;
        public AuthenticationController(IUserService userService, SignInManager<ApplicationUser> signInUser) 
        {
            _userService = userService;
            _signInUser = signInUser;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var mdLogin = new LoginModel();
            return View(mdLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage).ToList().ToList();

                ViewBag.Error = string.Join("<br/>", errors);

                return View();
            }

            var result = await _userService.CheckLogin(loginModel.UserName, loginModel.Password, loginModel.HasRemember);

            if (result.Status)
                return RedirectToAction("Index", "Home");

            ViewBag.Error = result.Message;

            return View(loginModel);
        }
        
      public async Task<IActionResult> Logout()
        {
            await _signInUser.SignOutAsync();
            return View("Login");
        }
    }
}

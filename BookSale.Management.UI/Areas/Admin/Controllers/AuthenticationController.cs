using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly SignInManager<ApplicationUser> _signInUser;
        public AuthenticationController(IAuthenticationService authenticationService, SignInManager<ApplicationUser> signInUser) 
        {
            _authenticationService = authenticationService;
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

            var result = await _authenticationService.CheckLogin(loginModel.UserName, loginModel.Password, loginModel.HasRemember);

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

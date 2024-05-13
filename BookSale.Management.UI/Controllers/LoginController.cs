using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace BookSale.Management.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly SignInManager<ApplicationUser> _signInUser;

        public LoginController(IAuthenticationService authenticationService, SignInManager<ApplicationUser> signInUser)
        {
            _authenticationService = authenticationService;
            _signInUser = signInUser;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var result = await _authenticationService.CheckLogin(loginModel.UserName, loginModel.Password, false);

            if (result.Status)
            {
                string returnUrl = loginModel.ReturnUrl;
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                    && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//")
                    && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("", "Home");
            }
            else
            {
                ViewBag.Error = result.Message;
                ModelState.AddModelError("error", "Username or password is incorrect");
            }

            return View(loginModel);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInUser.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

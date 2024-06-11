using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.ViewModal;
using BookSale.Management.DataAccess.Migrations;
using BookSale.Management.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v2;

namespace BookSale.Management.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IreCAPTCHASiteVerifyV2 _siteVerify;

        public LoginController(IAuthenticationService authenticationService,
                                SignInManager<ApplicationUser> signInManager,
                                UserManager<ApplicationUser> userManager,
                                IUserStore<ApplicationUser> userStore,
                                IreCAPTCHASiteVerifyV2 siteVerify)
        {
            _authenticationService = authenticationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _siteVerify = siteVerify;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginSiteModel loginModel)
        {
            if (String.IsNullOrEmpty(loginModel.Captcha))
            {
                ModelState.AddModelError("error", "Invalid captcha");
                return View(loginModel);
            }

            var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
            {
                Response = loginModel.Captcha,
                RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
            });

            if (!response.Success)
            {
                ModelState.AddModelError("error", "Invalid captcha");
                return View(loginModel);
            }

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

        public IActionResult External(string provider, string returnUrl = null)
        {
            var redirectUrl = $"/login/callbackexternal?handler=Callback&returnUrl={returnUrl}&remoteError=";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> CallBackExternal(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                string email = string.Empty;

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }

                var md = new ExternalLoginModel
                {
                    Provider = info.LoginProvider,
                    Email = email,
                    Fullname = info.Principal.Identity.Name,
                    ReturnUrl = returnUrl,
                };
                return View(md);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmationExternal(ExternalLoginModel externalLoginModel)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = externalLoginModel.ReturnUrl });
            }

            ModelState.Remove("ReturnUrl");

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, externalLoginModel.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        return RedirectToAction("", "home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("", "home");
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }
    }
}
using BookSale.Managament.Domain.Setting;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.User;
using BookSale.Management.Application.Services;
using BookSale.Management.UI.Models;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IBookService _bookService;

        public CheckOutController(IUserAddressService userAddressService, IBookService bookService)
        {
            _userAddressService = userAddressService;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string returnUrl)
        {
            IEnumerable<UserAdressDTO> addressDTOs;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var carts = GetSessionCart();

                if (carts is not null)
                {
                    var codes = carts.Select(x => x.BookCode).ToArray();

                    var books = await _bookService.GetBookByListCodeAsync(codes);

                    books = books.Select(book =>
                    {
                        var item = carts.SingleOrDefault(x => x.BookCode == book.Code);

                        if (item is not null)
                        {
                            book.Quantity = item.Quantity;
                        }
                        return book;
                    });

                    ViewBag.Books = books;
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                addressDTOs = await _userAddressService.GetUserAddressForSite(userId);
            }
            else 
            {
                return RedirectToAction("", "Login", new { returnUrl = returnUrl });
            }


            return View(addressDTOs);
        }
        private List<CartModel>? GetSessionCart()
        {
            return HttpContext.Session.Get<List<CartModel>>(CommonConstant.CartSessionName);
        }
    }
}

using BookSale.Managament.Domain.Enums;
using BookSale.Managament.Domain.Setting;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.Cart;
using BookSale.Management.Application.DTOs.Order;
using BookSale.Management.Application.DTOs.User;
using BookSale.Management.Application.Services;
using BookSale.Management.Infrastructure.Services;
using BookSale.Management.UI.Models;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Security.Claims;

namespace BookSale.Management.UI.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IBookService _bookService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly ICommonService _commonService;
        private readonly IEmailService _emailService;

        public CheckOutController(IUserAddressService userAddressService, 
            IBookService bookService,
            ICartService cartService,
            IOrderService orderService,
            ICommonService commonService,
            IEmailService emailService)
        {
            _userAddressService = userAddressService;
            _bookService = bookService;
            _cartService = cartService;
            _orderService = orderService;
            _commonService = commonService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(string returnUrl)
        {
            UserAddressDTO addressDTO = new();

            IEnumerable<UserAddressDTO> addressDTOs;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.Books = await GetCartFromSession();

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                addressDTOs = await _userAddressService.GetUserAddressForSite(userId);
                ViewBag.AddressDTOs = addressDTOs;
            }
            else 
            {
                return RedirectToAction("", "Login", new { returnUrl = returnUrl });
            }


            return View(addressDTO);
        }
        private List<CartModel>? GetSessionCart()
        {
            return HttpContext.Session.Get<List<CartModel>>(CommonConstant.CartSessionName);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteCart(UserAddressDTO userAddressDTO)
        {
            string codeOrder = $"ORDER_{DateTime.Now.ToString("ddMMyyyyhhmmss")}";
            if (ModelState.IsValid)
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;

                var books = await GetCartFromSession();

                userAddressDTO.UserId = userId;

                var addressId = await _userAddressService.SaveAsync(userAddressDTO);

                var cart = new CartRequestDTO
                {
                    CreatedOn = DateTime.Now,
                    Code = $"CART_{DateTime.Now.ToString("ddMMyyyyhhmmss")}",
                    Status = StatusProcessing.New,
                    Books = books.ToList()
                };

                var cartResult = await _cartService.Save(cart);

                double total = 0;

                foreach(BookCartDTO book in books)
                {
                    total += book.Price * book.Quantity;
                }

                if (cartResult)
                {
                    var order = new OrderRequestDTO
                    {
                        Books = books.ToList(),
                        CreatedOn = DateTime.Now,
                        Code = codeOrder,
                        PaymentMethod = userAddressDTO.PaymentMethod,
                        Status = StatusProcessing.New,
                        TotalAmount = total,
                        UserId = userId,
                        AddressId = addressId,
                        Id = userAddressDTO.PaymentMethod == PaymentMethod.Paypal ? userAddressDTO.OrderId : Guid.NewGuid().ToString(),
                    };

                    var orderResult = await _orderService.Save(order);


                    if (orderResult)
                    {
                        var emailInfo = new EmailSetting
                        {
                            Name = "Trum Da Den",
                            To = userAddressDTO.Email,
                            Subject = "Apply email API grateway Brevo",
                            Content = @"<h2>Checkout Complete</h2>
                                        <p>Thanks for your order! Your order number is: @ViewBag.OrderCode</p>",
                        };

                        await _emailService.Send(emailInfo);

                        HttpContext.Session.Remove(CommonConstant.CartSessionName);
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "Create order failed.");
                }

            }
            else
            {
                ModelState.AddModelError("error", "Process cart failed.");
            }

            ViewBag.OrderCode = codeOrder;

            return View();
        }

        private async Task<IEnumerable<BookCartDTO>> GetCartFromSession()
        {
            List<BookCartDTO> bookCartDTOs = new();
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

                bookCartDTOs = books.ToList();
            }

            return bookCartDTOs;
        }
    }
}

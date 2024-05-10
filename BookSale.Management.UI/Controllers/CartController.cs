using BookSale.Managament.Domain.Setting;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.UI.Models;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookSale.Management.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookService _bookService;

        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var carts = GetSessionCart();

            if (carts is not null)
            {
                ViewData["NumberCart"] = carts.Count;
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

                return View(books);
            }


            return View();
        }

        [HttpPost]
        public IActionResult Add(CartModel cart)
        {
            try
            {
                var carts = GetSessionCart() ?? new List<CartModel>();

                if (!carts.Any())
                {
                    carts.Add(cart);
                }
                else
                {
                    var cartExist = carts.SingleOrDefault(x => x.BookCode == cart.BookCode);

                    if (cartExist is null)
                    {
                        carts.Add(cart);
                    }
                    else
                    {
                        cartExist.Quantity += cart.Quantity;
                    }
                }

                SetSessionCart(carts);

                return Json(carts.Count);
            }
            catch (Exception)
            {

                return Json(-1);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] List<CartModel> books)
        {
            SetSessionCart(books);

            return Json(true);
        }

        [HttpPost]
        public IActionResult Delete(string code)
        {
            try
            {
                var carts = GetSessionCart();

                if (carts is not null)
                {
                    carts.RemoveAll(x => x.BookCode == code);

                    SetSessionCart(carts);
                }

                return Json(true);
            }
            catch (Exception)
            {

                return Json(false);
            }
        }

        private List<CartModel>? GetSessionCart()
        {
            return HttpContext.Session.Get<List<CartModel>>(CommonConstant.CartSessionName);
        }

        private void SetSessionCart(List<CartModel> carts)
        {
            HttpContext.Session.Set<List<CartModel>>(CommonConstant.CartSessionName, carts);
        }

    }
}

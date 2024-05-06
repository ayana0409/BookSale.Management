using BookSale.Management.Application.Abtracts;
using BookSale.Management.UI.Models;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionName = "CartSessionName";
        private readonly IBookService _bookService;

        public CartController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName);

            if (carts is not null) 
            { 
                var codes = carts.Select(x => x.BookCode).ToArray();

                var books = await _bookService.GetBookByListCodeAsync(codes);

                books = books.Select(book =>
                {
                    var item = carts.FirstOrDefault(x => x.BookCode == book.Code);

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
                var carts = HttpContext.Session.Get<List<CartModel>>(CartSessionName) ?? new List<CartModel>();

                if (!carts.Any())
                {
                    carts.Add(cart);
                }
                else
                {
                    var cartExist = carts.FirstOrDefault(x => x.BookCode == cart.BookCode);

                    if (cartExist is null)
                    {
                        carts.Add(cart);
                    }
                    else
                    {
                        cartExist.Quantity += cart.Quantity;
                    }
                }

                HttpContext.Session.Set(CartSessionName, carts);

                return Json(carts.Count);
            } 
            catch (Exception)
            {

                return Json(-1);
            }


        }
    }
}

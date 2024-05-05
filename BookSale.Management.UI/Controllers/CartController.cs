using BookSale.Management.UI.Models;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionName = "CartSessionName";
        public IActionResult Index()
        {
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

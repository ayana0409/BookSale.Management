using BookSale.Managament.Domain.Setting;
using BookSale.Management.Application.Abtracts;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Controllers
{
    public class ShopController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;

        public ShopController(IGenreService genreService, IBookService bookService)
        {
            _genreService = genreService;
            _bookService = bookService;
        }
        public async Task<IActionResult> Index(int g = 0, int idx = 1)
        {
            var genres = await _genreService.GetGenreForDropdownList();

            int pageSize = CommonConstant.BookPageSize;

            ViewBag.Genres = genres;

            var books = await _bookService.GetBookForSiteAsync(g, idx, pageSize);

            ViewBag.TotalItem = books.Item2;

            return View(books.Item1);
        }
    }
}

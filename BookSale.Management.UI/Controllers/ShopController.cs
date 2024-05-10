using BookSale.Managament.Domain.Entities;
using BookSale.Managament.Domain.Setting;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.UI.Models;
using BookSale.Management.UI.Ultility;
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
            var genres = _genreService.GetGenreListForSite();

            ViewBag.Genres = genres;
            ViewBag.CurrentGenre = g;
            ViewBag.CurrentPageIndex = idx;

            var result = await _bookService.GetBookForSiteAsync(g, idx, CommonConstant.BookPageSize);

            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetBookByPanigation(int genre, int pageIndex)
        {
            int pageSize = CommonConstant.BookPageSize;

            var result = await _bookService.GetBookForSiteAsync(genre, pageIndex, pageSize);

            return Json(result);
        }
    }
}

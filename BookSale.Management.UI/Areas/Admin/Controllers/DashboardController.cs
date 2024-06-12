using BookSale.Management.Application.Abtracts;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IGenreService _genreService;

        public DashboardController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [Breadscumb("Dashboard")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Genres = await _genreService.GetGenreForDropdownListAsync();

            return View();
        }
    }
}

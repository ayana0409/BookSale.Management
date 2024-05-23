using BookSale.Management.Application.Abtracts;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class ChartController : BaseController
    {
        private readonly IOrderService _orderService;

        public ChartController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetChartOrderByGenre(int genreId)
        {
            return Json(await _orderService.GetCharDataByGenreAsync(genreId));
        }
    }
}

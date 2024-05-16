using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Breadscumb("System", "Order List")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetByPagination(RequestDatatable requestDatatable)
        {
            var data = await _orderService.GetOrderByPagination(requestDatatable);

            return Json(data);
        }
    }
}

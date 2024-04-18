using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        [Breadscumb("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        //domain/Admin/Home/Index
    }
}

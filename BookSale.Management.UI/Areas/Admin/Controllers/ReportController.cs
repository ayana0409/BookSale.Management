using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs.Report;
using BookSale.Management.Infrastructure.Services;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IPDFService _pDFService;
        private readonly IOrderService _orderService;
        private readonly IGenreService _genreService;
        private readonly IExcelHandler _excelHandler;

        public ReportController(IPDFService pDFService, IOrderService orderService, IGenreService genreService,
                                    IExcelHandler excelHandler)
        {
            _pDFService = pDFService;
            _orderService = orderService;
            _genreService = genreService;
            _excelHandler = excelHandler;
        }
        [Breadscumb("Order Report")]
        public async Task<IActionResult> Index(ReportRequestDTO reportRequest)
        {
            IEnumerable<ReportOrderResponseDTO> responseDTOs = new List<ReportOrderResponseDTO>();

            var genres = await _genreService.GetGenreForDropdownList();
            ViewBag.Genres = genres;
            
            if(!string.IsNullOrEmpty(reportRequest.From) || !string.IsNullOrEmpty(reportRequest.To))
            {
                responseDTOs = await _orderService.GetReportOrderAsync(reportRequest);
            }

            ViewBag.FilterData = reportRequest;

            return View(responseDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> ExportPdfOrder(string id)
        {
            var order = await _orderService.GetReportByIdAsync(id);

            string html = await this.RenderViewAsync<ReportDTO>(RouteData , "_TemplateReportOrder", order, true);

            var result = _pDFService.GeneratePDF(html);

            return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcelOrder(ReportRequestDTO reportRequest)
        {
            var responseDTOs = await _orderService.GetReportOrderAsync(reportRequest);

            var stream = await _excelHandler.Export<ReportOrderResponseDTO>(responseDTOs.ToList());

            return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", $"OrderReport{DateTime.Now.Ticks}.xlsx");
        }
    }
}

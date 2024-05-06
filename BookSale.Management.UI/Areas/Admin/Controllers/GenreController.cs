using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModal;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Breadscumb("Genre List", "Application")]
        public IActionResult Index()
        {
            var genreMd = new GenreViewModal();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _genreService.GetById(id);
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> GetGenrePagination(RequestDatatable request)
        {
            var result = await _genreService.GetGenreByPagination(request);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(GenreViewModal genreViewModal)
        {
            ResponseModel responseModel = new()
            {
                Message = $"{(genreViewModal.Id == 0 ? "Insert" : "Update")} failes.",
                Status = false
            };
            if (ModelState.IsValid)
            {
                responseModel = await _genreService.Save(genreViewModal);
            }

            return Json(responseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _genreService.DeleteAsync(id));
        }
    }
}

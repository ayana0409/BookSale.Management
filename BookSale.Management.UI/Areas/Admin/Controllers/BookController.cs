using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModal;
using BookSale.Management.UI.Ultility;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.Areas.Admin.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;

        public BookController(IBookService bookService, IGenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
        }
        [Breadscumb("Book List", "Application")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Breadscumb("Book Form", "System")]
        public async Task<IActionResult> SaveData(int id)
        {
            var bookVM = new BookViewModal();
            var genreList = await _genreService.GetGenreForDropdownList();
            ViewBag.Genres = genreList;

            string code = await _bookService.GenerateCodeAsync();
            bookVM.Code = code;

            if (id != 0)
            {
                bookVM = await _bookService.GetById(id);
            }

            return View(bookVM);
        }

        [Breadscumb("Book Form", "System")]
        [HttpPost]
        public async Task<IActionResult> SaveData(BookViewModal bookViewModal)
        {
            var genreList = await _genreService.GetGenreForDropdownList();
            ViewBag.Genres = genreList;
            if (ModelState.IsValid)
            {
                var result = await _bookService.SaveAsync(bookViewModal);

                if (result.Status)
                {
                    return RedirectToAction("", "book");
                }
                else
                {
                    ModelState.AddModelError("error", "Invalid model");
                }

                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBookPagination(RequestDatatable request)
        {
            var result = await _bookService.GetBookByPagination(request);

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GenarateCodeBook()
        {
            var result = await _bookService.GenerateCodeAsync();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _bookService.DeleteAsync(id));
        }

    }
}

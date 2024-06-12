using BookSale.Management.Application.Abtracts;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.UI.ViewComponents
{
    [ViewComponent(Name ="GenreList")]
    public class GenreList : ViewComponent
    {
        private readonly IGenreService _genreService;

        public GenreList(IGenreService genreService)
        {
           _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isActive)
        {
            var genres = _genreService.GetGenreListForSiteAsync();

            return View(genres);
        }
    }
}

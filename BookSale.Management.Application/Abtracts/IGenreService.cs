using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Genre;
using BookSale.Management.Application.DTOs.ViewModal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abtracts
{
    public interface IGenreService
    {
        Task<bool> DeleteAsync(int id);
        Task<GenreViewModal> GetById(int id);
        Task<ResponseDatatable<GenreDTO>> GetGenreByPagination(RequestDatatable request);
        Task<IEnumerable<SelectListItem>> GetGenreForDropdownList();
        IEnumerable<GenreSiteDTO> GetGenreListForSite();
        Task<ResponseModel> Save(GenreViewModal genreDTO);
    }
}
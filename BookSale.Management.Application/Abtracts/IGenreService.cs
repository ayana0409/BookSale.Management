using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Genre;
using BookSale.Management.Application.DTOs.ViewModal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Abtracts
{
    public interface IGenreService
    {
        Task<bool> DeleteAsync(int id);
        Task<GenreViewModal> GetByIdAsync(int id);
        Task<ResponseDatatable<GenreDTO>> GetGenreByPaginationAsync(RequestDatatable request);
        Task<IEnumerable<SelectListItem>> GetGenreForDropdownListAsync();
        IEnumerable<GenreSiteDTO> GetGenreListForSiteAsync();
        Task<ResponseModel> SaveAsync(GenreViewModal genreDTO);
    }
}
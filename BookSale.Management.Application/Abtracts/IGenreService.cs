using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModal;

namespace BookSale.Management.Application.Abtracts
{
    public interface IGenreService
    {
        Task<bool> DeleteAsync(int id);
        Task<GenreViewModal> GetById(int id);
        Task<ResponseDatatable<GenreDTO>> GetGenreByPagination(RequestDatatable request);
        Task<ResponseModel> Save(GenreViewModal genreDTO);
    }
}
using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.Abtracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModal;
using BookSale.Management.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookSale.Management.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreViewModal> GetById(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);

            return _mapper.Map<GenreViewModal>(genre);
        }

        public async Task<ResponseDatatable<GenreDTO>> GetGenreByPagination(RequestDatatable request)
        {
            var genres = await _unitOfWork.GenreRepository.GetAllActiveGenre();

            var genresDTO = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            int totalRecord = genresDTO.Count();

            var result = genresDTO.Skip(request.SkipItems).Take(request.PageSize).ToList();

            return new ResponseDatatable<GenreDTO>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = totalRecord,
                Data = result
            };
        }

        public async Task<ResponseModel> Save(GenreViewModal genreDTO)
        {
            Genre genre = _mapper.Map<Genre>(genreDTO);
            var result = false;
            if (genre.Id == 0)
            {
                genre.IsActive = true;

                result = await _unitOfWork.GenreRepository.AddAsync(genre);

                if (result)
                {
                    return new ResponseModel
                    {
                        Action = Managament.Domain.Enums.ActionType.Insert,
                        Message = "Insert successful.",
                        Status = true,
                    };
                }
            }
            else
            {
                result = await _unitOfWork.GenreRepository.UpdateAsync(genre);

                if (result)
                {
                    return new ResponseModel
                    {
                        Action = Managament.Domain.Enums.ActionType.Update,
                        Message = "Update successful.",
                        Status = true,
                    };
                }
            }

            return new ResponseModel
            {
                Action = Managament.Domain.Enums.ActionType.Insert,
                Message = $"{(genre.Id == 0 ? "Insert" : "Update")} failes.",
                Status = false,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _unitOfWork.GenreRepository.FindById(id);
            if (genre is not null)
            {
                genre.IsActive = false;
                await _unitOfWork.GenreRepository.UpdateAsync(genre);

                return true;
            }
            return false;
        }
    }
}

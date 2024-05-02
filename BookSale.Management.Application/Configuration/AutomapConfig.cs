using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Books;
using BookSale.Management.Application.DTOs.Genre;
using BookSale.Management.Application.DTOs.ViewModal;

namespace BookSale.Management.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<ApplicationUser, AccountDTO>()
                .ForMember(dest => dest.Phone, source => source.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenreViewModal>().ReverseMap();
            CreateMap<Book, BookViewModal>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}

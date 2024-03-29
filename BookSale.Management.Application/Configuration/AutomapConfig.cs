using AutoMapper;
using BookSale.Managament.Domain.Entities;
using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<ApplicationUser, AccountDTO>()
                .ForMember(dest => dest.Phone, source => source.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
        }
    }
}

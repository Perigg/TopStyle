using AutoMapper;
using TopStyle.Domain.Entities;
using TopStyle.Domain.DTO;

namespace TopStyle.Domain.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
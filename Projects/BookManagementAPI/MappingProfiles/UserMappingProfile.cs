using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;

namespace BookManagementAPI.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserDTO, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<User, LoginUserDTO>();
        CreateMap<User, UserProfileDTO>();
        CreateMap<User, AuthResponseDTO>();
    }
}
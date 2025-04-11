using Application.DTOs.Auth;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // ApplicationUser to UserDTO
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.ErrorMessages, opt => opt.MapFrom(src => new List<string>()));

            // UserDTO to ApplicationUser
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()));

            // RegisterRequestDTO to ApplicationUser
            CreateMap<RegisterRequestDTO, ApplicationUser>()
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()));
        }
    }
}

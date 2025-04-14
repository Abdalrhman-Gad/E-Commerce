using AutoMapper;
using Domain.DTOs.Category;
using Domain.Models;

namespace Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image.FilePath)) // optional mapping if Image/FilePath needed
                .ReverseMap();

            CreateMap<Category, AddCategoryDTO>().ReverseMap();
        }
    }
}

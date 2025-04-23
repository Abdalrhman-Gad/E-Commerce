using Application.Mappings.BaseProfiles;
using Domain.DTOs.Product;
using Domain.Models;

namespace Application.Mappings
{
    public class ProductProfile
        : BaseMappingProfile<Product, AddProductDTO, GetProductDTO>
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDTO>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Image != null ? src.Image.FilePath : null))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ReverseMap();
        }
    }
}
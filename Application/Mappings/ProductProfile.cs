using AutoMapper;
using Domain.DTOs.Product;
using Domain.Models;

namespace Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, GetProductDTO>().ReverseMap();

            CreateMap<Product, AddProductDTO>().ReverseMap();
        }
    }
}

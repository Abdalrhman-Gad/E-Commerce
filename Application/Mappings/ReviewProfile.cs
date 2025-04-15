using AutoMapper;
using Domain.DTOs.Review;
using Domain.Models;

namespace Application.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review,AddReviewDTO>().ReverseMap();

            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using Domain.DTOs.Review;
using Domain.Models;

namespace Application.Mappings
{
    public class ReviewProfile :
        BaseMappingProfileWithUpdate<Review, AddReviewDTO, UpdateReviewDTO, ReviewDTO>
    {
        public ReviewProfile()
        {

        }
    }
}
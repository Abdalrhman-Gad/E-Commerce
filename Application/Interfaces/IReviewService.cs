using Domain.DTOs.Review;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IReviewService : IService<Review,AddReviewDTO,ReviewDTO>
    {

    }
}

using Application.Interfaces.IServices;
using Domain.DTOs.Review;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IReviewService :
        IReadableService<Review, ReviewDTO>,
        ICreatableService<AddReviewDTO, ReviewDTO>,
        IUpdatableService<ReviewDTO, UpdateReviewDTO>,
        IDeletableService<string>
    {

    }
}

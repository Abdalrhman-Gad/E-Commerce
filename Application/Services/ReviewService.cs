using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Review;
using Domain.Exceptions.Review;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ReviewDTO> AddAsync(AddReviewDTO addReview)
        {
            if (addReview == null) throw new InvalidReviewException("Review data is invalid");

            var review = _mapper.Map<Review>(addReview);

            try
            {
                await _reviewRepository.AddAsync(review);

                return _mapper.Map<ReviewDTO>(review);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("An error occurred while adding the review.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(r => r.ReviewId == id) ??
                throw new ReviewNotFoundException("Review not found.");

            await _reviewRepository.DeleteAsync(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllAsync(Expression<Func<Review, bool>>? filter = null, string? includes = null, int pageSize = 0, int pageNumber = 1)
        {
            var reviews = await _reviewRepository.GetAllAsync(filter, includes, pageSize, pageNumber);

            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> GetByIdAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(r => r.ReviewId == id);

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> UpdateAsync(int id, AddReviewDTO addReview)
        {
            var review = await _reviewRepository.GetAsync(r => r.ReviewId == id) ??
                throw new ReviewNotFoundException("Review not found.");

            await _reviewRepository.UpdateAsync(review);

            return _mapper.Map<ReviewDTO>(review);
        }
    }
}

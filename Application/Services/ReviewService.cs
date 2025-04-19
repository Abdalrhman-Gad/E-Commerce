using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.Review;
using Domain.Exceptions.Review;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ReviewService(
            IReviewRepository reviewRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ReviewDTO> AddAsync(AddReviewDTO addReview)
        {
            if (addReview == null)
                throw new InvalidReviewException("Review data is invalid");

            var review = _mapper.Map<Review>(addReview);
            review.UserId = GetCurrentUserId();

            try
            {
                await _reviewRepository.AddAsync(review);

                return _mapper.Map<ReviewDTO>(review);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx &&
                                                (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                throw new ReviewAlreadySubmittedException("You have already submitted a review for this product.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the review.", ex);
            }
        }

        public async Task DeleteAsync(string userId)
        {
            var review = await _reviewRepository.GetAsync(
                r => r.UserId == GetCurrentUserId())
                ?? throw new ReviewNotFoundException("Review not found.");

            await _reviewRepository.DeleteAsync(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllAsync(
            Expression<Func<Review, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
        {
            var reviews = await _reviewRepository.GetAllAsync(
                filter: filter,
                pageSize: pageSize,
                pageNumber: pageNumber
            );

            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> GetByIdAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(r => r.ReviewId == id)
                ?? throw new ReviewNotFoundException("Review not found.");

            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> UpdateAsync(int id, UpdateReviewDTO updateReview)
        {
            var review = await _reviewRepository.GetAsync(
                r => r.ReviewId == id && r.UserId == GetCurrentUserId())
                ?? throw new ReviewNotFoundException("Review not found.");

            _mapper.Map(updateReview, review);

            await _reviewRepository.UpdateAsync(review);

            return _mapper.Map<ReviewDTO>(review);
        }

        private string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId ?? throw new UnauthorizedAccessException("User ID not found.");
        }
    }
}
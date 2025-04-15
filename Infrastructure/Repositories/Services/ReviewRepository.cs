using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Services
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _db;

        public ReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
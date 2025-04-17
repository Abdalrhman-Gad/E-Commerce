using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Services
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
    }
}

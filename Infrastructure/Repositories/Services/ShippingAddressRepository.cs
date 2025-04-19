using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Services
{
    public class ShippingAddressRepository : Repository<ShippingAddress>, IShippingAddressRepository
    {
        public ShippingAddressRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
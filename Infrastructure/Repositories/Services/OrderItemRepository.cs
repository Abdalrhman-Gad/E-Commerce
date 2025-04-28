using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Services
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

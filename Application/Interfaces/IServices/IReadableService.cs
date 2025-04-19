using System.Linq.Expressions;

namespace Application.Interfaces.IServices
{
    public interface IReadableService<T, TGet>
    {
        Task<TGet> GetByIdAsync(int id);
        Task<IEnumerable<TGet>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1);
    }
}

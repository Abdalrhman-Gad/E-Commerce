using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IService<T, TAdd, TGet>
        where T : class
        where TAdd : class
        where TGet : class
    {
        Task<TGet> GetByIdAsync(int id);

        Task<IEnumerable<TGet>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1);

        Task<TGet> AddAsync(TAdd entity);

        Task DeleteAsync(int id);
    }
}
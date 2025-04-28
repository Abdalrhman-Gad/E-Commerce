using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includes = null);
       
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includes = null, int pageSize = 0, int pageNumber = 1);
        
        Task<T> AddAsync(T entity);
        
        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);
    }
}

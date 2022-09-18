using System.Linq.Expressions;

namespace VerdeBordo.Core.Persistence.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
         Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
         Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
         Task AddAsync(T entity);
         Task UpdateAsync(T entity);
         Task DeleteAsync(T entity);
         Task<bool> ExistAsync(int id);
    }
}
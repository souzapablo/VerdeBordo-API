namespace VerdeBordo.Core.Persistence.Interfaces.Base
{
    public interface IBaseRepository<T>
    {
         Task<List<T>> GetAllAsync();
         Task<T?> GetByIdAsync(int id);
         Task AddAsync(T entity);
         Task UpdateAsync(T entity);
         Task DeleteAsync(T entity);
         Task<bool> ExistAsync(int id);
    }
}
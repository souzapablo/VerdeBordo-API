using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VerdeBordo.Core.Entities.Base;
using VerdeBordo.Core.Persistence.Interfaces.Base;

namespace VerdeBordo.Infrastructure.Persistence.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly VerdeBordoDbContext _dbContext;

        public BaseRepository(VerdeBordoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.Where(x => x.Id.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            var entity = await _dbContext.Set<T>()
                .SingleOrDefaultAsync(x => x.Id == id);

            return entity is not null ? true : false;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
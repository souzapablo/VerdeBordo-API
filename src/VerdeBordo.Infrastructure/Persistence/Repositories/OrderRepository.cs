using Microsoft.EntityFrameworkCore;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Interfaces.Repositories;
using VerdeBordo.Infrastructure.Persistence.Repositories.Base;

namespace VerdeBordo.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(VerdeBordoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Order>> GetOrdersByClentIdAsync(int clientId)
        {
            return await _dbContext.Orders
                .Where(x => x.ClientId == clientId)
                .Include(x => x.Embroideries)
                .Include(x => x.Payments)
                .ToListAsync();
        }
    }
}
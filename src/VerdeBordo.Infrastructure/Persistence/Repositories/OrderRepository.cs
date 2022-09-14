using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Persistence.Interfaces;
using VerdeBordo.Infrastructure.Persistence.Repositories.Base;

namespace VerdeBordo.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(VerdeBordoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
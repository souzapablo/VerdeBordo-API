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
    }
}
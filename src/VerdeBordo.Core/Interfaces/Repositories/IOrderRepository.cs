using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Persistence.Interfaces.Base;

namespace VerdeBordo.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrdersByClentIdAsync(int clientId);
    }
}
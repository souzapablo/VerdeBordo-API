using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Interfaces.Repositories;
using VerdeBordo.Infrastructure.Persistence.Repositories.Base;

namespace VerdeBordo.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(VerdeBordoDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
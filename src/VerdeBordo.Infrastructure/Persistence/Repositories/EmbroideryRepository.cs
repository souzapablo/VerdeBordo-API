using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Interfaces.Repositories;
using VerdeBordo.Infrastructure.Persistence.Repositories.Base;

namespace VerdeBordo.Infrastructure.Persistence.Repositories
{
    public class EmbroideryRepository : BaseRepository<Embroidery>, IEmbroideryRepository
    {
        public EmbroideryRepository(VerdeBordoDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
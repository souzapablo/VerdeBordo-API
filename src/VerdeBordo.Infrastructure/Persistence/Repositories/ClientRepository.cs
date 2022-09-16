using Microsoft.EntityFrameworkCore;
using VerdeBordo.Core.Entities;
using VerdeBordo.Core.Interfaces.Repositories;
using VerdeBordo.Infrastructure.Persistence.Repositories.Base;

namespace VerdeBordo.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(VerdeBordoDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
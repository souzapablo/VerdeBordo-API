using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VerdeBordo.Core.Entities;

namespace VerdeBordo.Infrastructure.Persistence
{
    public class VerdeBordoDbContext : DbContext
    {
        public VerdeBordoDbContext(DbContextOptions<VerdeBordoDbContext> options) : base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; } = null!; 
        public DbSet<Embroidery> Embroideries { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerdeBordo.Core.Entities;

namespace VerdeBordo.Infrastructure.Persistence.Configurations
{
    public class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId);
        }
    }
}
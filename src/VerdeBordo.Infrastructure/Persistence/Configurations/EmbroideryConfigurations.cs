using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerdeBordo.Core.Entities;

namespace VerdeBordo.Infrastructure.Persistence.Configurations
{
    public class EmbroideryConfigurations : IEntityTypeConfiguration<Embroidery>
    {
        public void Configure(EntityTypeBuilder<Embroidery> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Price)
                .HasPrecision(18, 4);
        }
    }
}
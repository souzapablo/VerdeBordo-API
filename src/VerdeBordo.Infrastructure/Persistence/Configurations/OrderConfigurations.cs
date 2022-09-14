using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerdeBordo.Core.Entities;

namespace VerdeBordo.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            builder.HasMany(o => o.Embroideries)
                .WithOne()
                .HasForeignKey(e => e.Id);

            builder.HasMany(o => o.Payments)
                .WithOne()
                .HasForeignKey(p => p.Id);

            builder.Property(o => o.DeliveryFee)
                .HasPrecision(18, 4);

            builder.Property(o => o.OrderPrice)
                .HasPrecision(18, 4);

            builder.Property(o => o.PayedAmount)
                .HasPrecision(18, 4);                
        }
    }
}
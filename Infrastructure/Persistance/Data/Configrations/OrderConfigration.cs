global using OrderEntity = Domain.Models.OrderEntities.Order;
using Domain.Models.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    internal class OrderConfigration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress,
                address => address.WithOwner());

            builder.HasMany(o => o.OrderItems)
                .WithOne();

            builder.Property(o => o.PaymentStatus)
                .HasConversion(
                s => s.ToString(),
                s => Enum.Parse<OrderPaymentStatus>(s)
                );

            builder.HasOne(o => o.DelveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(p => p.Subtotal).
                HasColumnType("decimal(18,3)");
        }
    }
}

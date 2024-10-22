using Domain.Models.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    internal class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.Property(p => p.Price).
                HasColumnType("decimal(18,3)");

            builder.OwnsOne(item => item.Product,
                product => product.WithOwner());
        }
    }
}

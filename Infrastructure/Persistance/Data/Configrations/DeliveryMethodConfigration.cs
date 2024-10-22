using Domain.Models.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    internal class DeliveryMethodConfigration : IEntityTypeConfiguration<DelveryMethod>
    {
        public void Configure(EntityTypeBuilder<DelveryMethod> builder)
        {
            builder.Property(p => p.Price).
                HasColumnType("decimal(18,3)");
        }
    }
}

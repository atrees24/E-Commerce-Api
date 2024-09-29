using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                ; 

            builder.HasOne(p => p.Type)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.TypeId)
                ; 

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,3)");
        }
    }
}

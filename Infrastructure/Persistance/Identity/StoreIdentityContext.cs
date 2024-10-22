global using UserAddress = Domain.Models.Address;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Persistance.Identity
{
    public class StoreIdentityContext : IdentityDbContext
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAddress>().ToTable("Addresses");
        }
    }
}

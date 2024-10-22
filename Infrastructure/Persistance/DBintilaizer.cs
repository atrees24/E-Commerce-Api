using Domain.Interfaces;
using Domain.Models.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DBintilaizer : IDBintilaizer
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBintilaizer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initilaize()
        {
            try
            {
                if ((await _storeContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _storeContext.Database.MigrateAsync();
                }


                if (!_storeContext.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.ProductBrands.Any())
                {
                    var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    if (brands is not null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.products.Any())
                {
                    var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\products.json");

                    var product = JsonSerializer.Deserialize<List<Product>>(productData);

                    if (product is not null && product.Any())
                    {
                        await _storeContext.products.AddRangeAsync(product);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                //if (!_storeContext.DelveryMethods.Any())
                //{
                //    var deliveryData = await File.
                //        ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\Seeding\delivery.json");

                //    var delveryMethods = JsonSerializer.Deserialize<List<DelveryMethod>>(deliveryData);

                //    if (delveryMethods is not null && delveryMethods.Any())
                //    {
                //        await _storeContext.DelveryMethods.AddRangeAsync(delveryMethods);
                //        await _storeContext.SaveChangesAsync();
                //    }
                //}

            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task InitilaizeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if(!_userManager.Users.Any())
            {
                var SuperAdminUser = new User
                {
                    DisplayName = "Super Admin User",
                    Email = "SuperAdminUser@gmail.com",
                    UserName = "SuperAdminUser",
                    PhoneNumber = "1234567890"
                };

                var AdminUser = new User
                {
                    DisplayName = "Admin User",
                    Email = "AdminUser@gmail.com",
                    UserName = "AdminUser",
                    PhoneNumber = "1234567890"
                };

                await _userManager.CreateAsync(SuperAdminUser,"Passw0rd");
                await _userManager.CreateAsync(AdminUser, "Passw0rd");

                await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
            }
        }
    }
}

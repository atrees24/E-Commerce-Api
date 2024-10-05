using Domain.Interfaces;
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

        public DBintilaizer(StoreContext storeContext)
        {
            _storeContext = storeContext;
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

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}

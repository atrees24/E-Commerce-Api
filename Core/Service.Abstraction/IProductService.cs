using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IProductService
    {

        public Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync();
        public Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync();
        public Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync();

        public Task<ProductResultDTO?> GetProductByIdAsync(int id);
    }
}

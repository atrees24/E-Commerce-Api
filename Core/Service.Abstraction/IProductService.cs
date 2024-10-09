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

        public Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpceficationParamters paramters);
        public Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync();
        public Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync();

        public Task<ProductResultDTO?> GetProductByIdAsync(int id);
    }
}

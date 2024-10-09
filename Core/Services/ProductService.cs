using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Service.Abstraction;
using Services.Spcefications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUniteOfWork UniteOfWork , IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync()
        {
            var brands =  await UniteOfWork.GetRepository<ProductBrand,int>().GetAllAsync();

            var brandResult = Mapper.Map<IEnumerable<ProductBrandDTO>>(brands);

            return brandResult;
        }

        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpceficationParamters paramters)
        {
            var products = await UniteOfWork.GetRepository<Product,int>()
                .GetAllAsync(new ProductWithBrandAndTypeSpcefications(paramters));

            var productResult = Mapper.Map<IEnumerable<ProductResultDTO>>(products);
            var count = productResult.Count();
            var totalCount = await UniteOfWork.GetRepository<Product, int>()
                .CountAsync(new ProductCountSpcefications(paramters));


            var result = new PaginatedResult<ProductResultDTO>
           (paramters.PageIndex, count, totalCount, productResult);
           return result;
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync()
        {
            var types = await UniteOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var typeResult = Mapper.Map<IEnumerable<ProductTypeDTO>>(types);

            return typeResult;
        }

        public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
        {
            var product = await UniteOfWork.GetRepository<Product,int>()
                .GetAsync(new ProductWithBrandAndTypeSpcefications(id));
            return product is null ? throw new ProductNotFoundException(id)
                : Mapper.Map<ProductResultDTO>(product);
        }
    }
}

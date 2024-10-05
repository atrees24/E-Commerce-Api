using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Service.Abstraction;
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

        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync()
        {
            var products = await UniteOfWork.GetRepository<Product,int>().GetAllAsync();

            var productResult = Mapper.Map<IEnumerable<ProductResultDTO>>(products);

            return productResult;
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync()
        {
            var types = await UniteOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var typeResult = Mapper.Map<IEnumerable<ProductTypeDTO>>(types);

            return typeResult;
        }

        public async Task<ProductResultDTO?> GetProductByIdAsync(int id)
        {
            var product = await UniteOfWork.GetRepository<Product,int>().GetAsync(id);

            var productResult = Mapper.Map<ProductResultDTO>(product);

            return productResult;
        }
    }
}

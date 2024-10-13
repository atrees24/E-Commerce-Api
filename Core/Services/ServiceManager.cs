using AutoMapper;
using Domain.Interfaces;
using Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        public ServiceManager(IUniteOfWork UniteOfWork , IMapper Mapper , IBasketRepository basketRepository)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(UniteOfWork, Mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, Mapper));
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService basketService => _basketService.Value;
    }
}

using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Abstraction;
using Shared;
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
        private readonly Lazy<IAuthentactionService> _authentactionService;
        private readonly Lazy<IOrderService> _orderService;
        public ServiceManager(IUniteOfWork UniteOfWork, IMapper Mapper, IBasketRepository basketRepository,
            UserManager<User> userManager, IOptions<JwtOptions> options)
        {
            _productService = new Lazy<IProductService>(
                () => new ProductService(UniteOfWork, Mapper));
            _basketService = new Lazy<IBasketService>(
                () => new BasketService(basketRepository, Mapper));
            _authentactionService = new Lazy<IAuthentactionService>(
                () => new AuthentactionService(userManager, options,Mapper));
            _orderService = new Lazy<IOrderService>(
                () => new OrderService(UniteOfWork, Mapper,basketRepository));
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService basketService => _basketService.Value;

        public IAuthentactionService athentcationService => _authentactionService.Value;

        public IOrderService orderService => _orderService.Value;
    }
}

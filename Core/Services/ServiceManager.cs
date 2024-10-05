﻿using AutoMapper;
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

        public ServiceManager(IUniteOfWork UniteOfWork , IMapper Mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(UniteOfWork, Mapper));
        }

        public IProductService ProductService => _productService.Value;
    }
}

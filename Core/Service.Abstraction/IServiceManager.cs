﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService basketService { get; }
        public IAuthentactionService athentcationService { get; }
    }
}

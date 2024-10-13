using AutoMapper;
using Domain.Exceptions;
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
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) 
        : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
        => await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) 
                : mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var UpdatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);
            return UpdatedBasket is null ? throw new Exception("Can't Update Now")
                : mapper.Map<BasketDTO>(customerBasket);
        }
    }
}

using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket , TimeSpan? TimeToLive = null);
        public Task<bool> DeleteBasketAsync(string id);
    }
}

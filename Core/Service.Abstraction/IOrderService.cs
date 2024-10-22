using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public  interface IOrderService
    {
        public Task<OrderResult> GetOrderByIdAsync(Guid id);
        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email);
        public Task<OrderResult> CreateOrderAsync(OrderRequst requst, string userEmail);
        public Task<IEnumerable<DelveryMethodResult>> GetDelveryMethodsAsync();
    }
}

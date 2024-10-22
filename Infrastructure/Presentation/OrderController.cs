using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) :ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequst request)
        {
            var email = User.FindFirstValue( ClaimTypes.Email);

            var order = await serviceManager.orderService.CreateOrderAsync(request,  email);

            return Ok( order);
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var orders = await serviceManager.orderService.GetOrdersByEmailAsync(email);

            return Ok(orders);
        }

        [HttpGet( "id")]
        public async Task<ActionResult<OrderResult>> GetOrder(Guid id)
        {
            var order = await serviceManager.orderService.GetOrderByIdAsync(id);

            return Ok(order);
        }

        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<DelveryMethodResult>> GetDeliveryMethod()
        {
            var Method = await serviceManager.orderService.GetDelveryMethodsAsync();
            return Ok(Method);
        }

    }
}

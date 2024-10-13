using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager serviceManager)
        : ControllerBase
    {
        [HttpGet("{id}") ]
        public async Task<ActionResult<BasketDTO>> Get(string id)
         {
            var basket = await serviceManager.basketService.GetBasketAsync(id); 
            return Ok(basket);
          }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDTO)
        {
            var basket = await serviceManager.basketService.UpdateBasketAsync( basketDTO); 
            return Ok( basket);
        }


        [HttpDelete(template: "{id}")] 
         public async Task<ActionResult> Delete(string id)
        {
            await serviceManager.basketService.DeleteBasketAsync( id);
            return NoContent();
        }
    }
}

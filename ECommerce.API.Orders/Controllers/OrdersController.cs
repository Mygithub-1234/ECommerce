using ECommerce.API.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public IOrderProvider orderProvider { get; private set; }
        public OrdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
                
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await orderProvider.GetOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NoContent();
        }


        
    }
}

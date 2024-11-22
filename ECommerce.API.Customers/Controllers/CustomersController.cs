using ECommerce.API.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerProvider customerprovider;

        public CustomersController(ICustomerProvider customerProvider)
        {
            
            this.customerprovider = customerProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customerprovider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            return BadRequest("Not found");

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customerprovider.GetCustomerAync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            return BadRequest("Not found");


        }
    }
}

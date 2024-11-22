using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class CustomersService:ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<Customer> logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<Customer> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> ICustomersService.GetCustomersAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

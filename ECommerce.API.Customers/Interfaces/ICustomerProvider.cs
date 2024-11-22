using ECommerce.API.Customers.Models;

namespace ECommerce.API.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync();

        Task<(bool IsSuccess, Customer Customer, string ErroMessage)> GetCustomerAync(int id);
    }
}

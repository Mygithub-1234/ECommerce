using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSucess, IEnumerable<Product> Products, string ErrorMessage)>
            GetProductsAsync();
    }
}

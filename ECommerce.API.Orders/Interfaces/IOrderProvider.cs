namespace ECommerce.API.Orders.Interfaces
{
    public interface IOrderProvider
    {
        public Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrdersAsync(int CustomerId);
    }
}

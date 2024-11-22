using ECommerce.API.Search.Interfaces;

namespace ECommerce.API.Search.Services
{
    public class Services : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public Services(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsync();
            var customerResult = await customersService.GetCustomersAsync(customerId);
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSucess ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product Info not available";
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new {Name ="Customer info not available"},
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}

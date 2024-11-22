using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private ProductsDbContext dbContext;
        private ILogger<ProductsProvider> logger;
        private IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {

            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();

        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new DB.Product() { Id = 1, Name = "Monitor", Price = 50, Inventory = 100 });
                dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Keyboard", Price = 50, Inventory = 100 });
                dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Mouse", Price = 10, Inventory = 100 });
                dbContext.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 10, Inventory = 100 });
                dbContext.SaveChanges();
            }
        }


        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {

            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErroMessage)> GetProductAync(int id)
        {
            try
            {
                var products = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (products != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

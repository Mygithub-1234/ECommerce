using AutoMapper;
using ECommerce.API.Customers.DB;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private CustomerDbContext dbContext;
        private ILogger<ICustomerProvider> logger;
        private IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext, ILogger<ICustomerProvider> logger, IMapper mapper) 
        { 
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
           
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "John", Address="Concord" });
                dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "Jia", Address = "Florida" });
                dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "Adam", Address = "Alabama" });
                dbContext.Customers.Add(new DB.Customer() { Id = 4, Name = "Mary", Address = "Seattle" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErroMessage)> GetCustomerAync(int id)
        {
            try
            {
                var customers = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customers != null)
                {
                    var result = mapper.Map<DB.Customer, Models.Customer>(customers);
                    return (true, result, "Success");
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(customers);
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

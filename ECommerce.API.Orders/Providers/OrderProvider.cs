using AutoMapper;
using ECommerce.API.Orders.DB;
using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerce.API.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        public OrderDbContext dbContext { get; set; }
        public ILogger<OrderProvider> logger { get; set; }
        public IMapper mapper { get; set; }

        public OrderProvider(OrderDbContext dbContext, ILogger<OrderProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();

        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(10),
                    Total = 200,
                    Items = new List<DB.OrderItem>{ new DB.OrderItem { Id = 1, ProductId = 1, Quantity = 1, UnitPrice = 12.4M },
                                                    new DB.OrderItem { Id = 2, ProductId = 2, Quantity = 2, UnitPrice = 25.5M },
                                                    new DB.OrderItem { Id =3, ProductId =3, Quantity =3, UnitPrice =50.25M }}
                });

                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(10),
                    Total = 200,
                    Items = new List<DB.OrderItem>{ new DB.OrderItem { Id = 4, ProductId = 1, Quantity = 1, UnitPrice = 12.4M },
                                                    new DB.OrderItem { Id = 5, ProductId = 2, Quantity = 2, UnitPrice = 25.5M },
                                                    new DB.OrderItem {Id =  6, ProductId = 3, Quantity = 3, UnitPrice = 50.25M }}
                });

                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDate = DateTime.Now.AddDays(10),
                    Total = 200,
                    Items = new List<DB.OrderItem>{ new DB.OrderItem { Id = 7, ProductId = 1, Quantity = 1, UnitPrice = 12.4M },
                                                    new DB.OrderItem { Id = 8, ProductId = 2, Quantity = 2,  UnitPrice = 25.5M },
                                                    new DB.OrderItem { Id =9, ProductId =  3, Quantity =3,   UnitPrice =50.25M }}
                });

                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 4,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(10),
                    Total = 200,
                    Items = new List<DB.OrderItem>{ new DB.OrderItem { Id = 10, ProductId = 1, Quantity = 1, UnitPrice = 12.4M },
                                                    new DB.OrderItem { Id = 11, ProductId = 2, Quantity = 2, UnitPrice = 25.5M },
                                                    new DB.OrderItem { Id = 12, ProductId =3, Quantity =  3, UnitPrice =50.25M }}
                });

                dbContext.SaveChanges();

            }

        }

            public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrdersAsync(int CustomerId)
            {
                try
                {
                    var orders = await dbContext.Orders.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
                    if (orders != null)
                    {
                        var result = mapper.Map<DB.Order, Models.Order>(orders);
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

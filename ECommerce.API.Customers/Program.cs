using ECommerce.API.Customers.DB;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerProvider, CustomerProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseInMemoryDatabase("Customer");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

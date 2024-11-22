using ECommerce.API.Orders.DB;
using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IOrderProvider, OrderProvider>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseInMemoryDatabase("Orders");
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

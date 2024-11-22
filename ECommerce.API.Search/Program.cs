using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISearchService, Services>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped <CustomersService,CustomersService > ();
builder.Services.AddHttpClient("OrdersService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);

});
builder.Services.AddHttpClient("ProductsService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services.Products"]);
}).AddTransientHttpErrorPolicy(p=> p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("CustomersService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services.Customers"]);
}).AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(5, _=> TimeSpan.FromMilliseconds(500)));

builder.Services.AddControllers();
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

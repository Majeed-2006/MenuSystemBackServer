using BusinessLayer.Services;
using BusinessLayer.Utitlity;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<AppDbContext>();

builder.Services.AddScoped<CustomerDataAccess>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped<UserDataAccess>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderDataAccess>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductDataAccess>();





builder.Services.AddScoped<OrderItemDataAccess>();
builder.Services.AddScoped<OrderItemService>();

builder.Services.AddScoped<OrderStatusDataAccess>();
builder.Services.AddScoped<OrderStatusService>();

builder.Services.AddScoped<RestaurantDataAccess>();
builder.Services.AddScoped<RestaurantService>();

//builder.Services.AddControllers();
builder.Services.AddControllers()
                .AddNewtonsoftJson(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddBusinessServices(connectionString);

//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();

app.UseExceptionHandler(options => { });

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

using OrderService.Services;
using System;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repositories;
using OrderService.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// HttpClient for ProductService
builder.Services.AddHttpClient<ProductHttpClient>(client =>
{
    
    string baseUrl = builder.Environment.IsDevelopment()
        ? "https://localhost:5154" // Update this with your actual port
        : "http://productservice:8080";
    
    client.BaseAddress = new Uri(baseUrl);
});


//HttpClient for UserService
builder.Services.AddHttpClient<UserHttpClient>(client =>
{
    
    string baseUrl = builder.Environment.IsDevelopment()
        ? "https://localhost:5283" 
        : "http://userservice:8080"; // Docker service name

    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
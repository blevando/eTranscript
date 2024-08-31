using eTranscript.Data;
using eTranscript.Managers;
using eTranscript.Services.Interfaces;
using eTranscript.Services.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInventoryManagement, eTranscript.Services.Repositories.InventoryManagement>();

// Registrering Managers
builder.Services.AddScoped<InventoryManager>();
builder.Services.AddScoped<OrderManager>();


//Registering Services (Repositories)
builder.Services.AddScoped<IInventoryManagement, InventoryManagement>();
builder.Services.AddScoped<IOrderManagement, OrderManagement>();

builder.Services.AddDbContext<ApplicationDbContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));

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

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

// Registrering Managers
builder.Services.AddScoped<InventoryManager>();
builder.Services.AddScoped<OrderManager>();
builder.Services.AddScoped<PaymentFactoryManager>();


//Registering Services (Repositories)
builder.Services.AddScoped<IInventoryManagement, InventoryManagement>();
builder.Services.AddScoped<IOrderManagement, OrderManagement>();
builder.Services.AddScoped<IInventoryManagement, InventoryManagement>();

builder.Services.AddScoped<IPaymentFactoryManagement, PaymentFactoryManagement>();
 
builder.Services.AddScoped<IPaymentManagement, CyberPayProcessor>();
builder.Services.AddScoped<IPaymentManagement, FlutterwaveProcessor>();
builder.Services.AddScoped<IPaymentManagement, OtherPaymentProcessor>();



builder.Services.AddDbContext<ApplicationDbContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.WithOrigins("http://localhost:3000", "http://localhost:3000");
    x.AllowAnyMethod();
    x.AllowAnyHeader();

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

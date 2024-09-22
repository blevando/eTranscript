using eTranscript.Data;
using eTranscript.Managers;
using eTranscript.Models.EntityModels;
using eTranscript.Services.Interfaces;
using eTranscript.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

 
using Microsoft.AspNetCore.Authentication.JwtBearer;
 
 
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));


// Add services to the container.
// 2. Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// 4. Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"].ToString()))
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrering Managers
builder.Services.AddScoped<InventoryManager>();
builder.Services.AddScoped<OrderManager>();
builder.Services.AddScoped<PaymentFactoryManager>();
builder.Services.AddScoped<AuthenticationManager>();

//Registering Services (Repositories)
//builder.Services.AddScoped<IInventoryManagement, InventoryManagement>();
builder.Services.AddScoped<IOrderManagement, OrderManagement>();
builder.Services.AddScoped<IInventoryManagement, InventoryManagement>();
builder.Services.AddScoped<IAuthenticationManagement, AuthenticationManagement>();
builder.Services.AddScoped<IPaymentFactoryManagement, PaymentFactoryManagement>();
 
builder.Services.AddScoped<IPaymentManagement, CyberPayProcessor>();
builder.Services.AddScoped<IPaymentManagement, FlutterwaveProcessor>();
builder.Services.AddScoped<IPaymentManagement, OtherPaymentProcessor>();

 
       
        


var app = builder.Build();

// Configure the HTTP request pipeline
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

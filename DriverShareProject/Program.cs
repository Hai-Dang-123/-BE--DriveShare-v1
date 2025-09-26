using DAL.Context;
using DriverShareProject.Extentions.PolicyExtensions;
using DriverShareProject.Extentions.ServiceRegistration;
using DriverShareProject.Extentions.Startup;
using Microsoft.EntityFrameworkCore;
using DriverShareProject.Extentions.BuilderExtensions;
using BLL.Services.Implement;
using BLL.Services.Interface;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

// Add services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Program.cs hoặc Startup.cs (trong ConfigureServices)
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.RegisterAllServices(builder.Configuration);

//Add config
//builder.AddAppConfiguration();

//Add CORS policy
builder.Services.AddAuthorizationPolicies();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add custom middlewares
app.UseApplicationMiddlewares();

// Add CORS
app.UseCorsPolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Add Routing
//app.MapCustomEndpoints();

app.Run();


using eVoucher_DAL;
using eVoucher_DAL.Repositories;
using eVoucher_DAL.InfraStructure;
using eVoucher_DTO.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using eVoucher_BUS.Services;
using eVoucherDatabaseWebService;
/*
 * original ASPNetCore Program
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<eVoucherDbContext>();
builder.Services.AddTransient<GameRepository>();
builder.Services.AddTransient<GameService>();
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
*/
//Separate configuration in startup.cs
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

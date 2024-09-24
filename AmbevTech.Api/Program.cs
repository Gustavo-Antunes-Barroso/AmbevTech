using AmbevTech.Application.Interfaces;
using AmbevTech.Application.Services;
using AmbevTech.Domain.Interfaces;
using AmbevTech.Infrastructure.Context;
using AmbevTech.Infrastructure.EventBus;
using AmbevTech.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer("ConnectionString"));

        services.AddScoped<IVendaRepository, VendaRepository>();
        services.AddScoped<IVendaService, VendaService>();

        services.AddEventBus(builder =>
        {
            builder.AddRabbitMqTransport(options =>
            {
                options.Hostname= "";
                options.Username = "";
                options.Password = "";
            });
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
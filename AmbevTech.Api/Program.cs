using AmbevTech.Api.Middleware;
using AmbevTech.Application.Interfaces;
using AmbevTech.Application.Services;
using AmbevTech.Domain.Interfaces;
using AmbevTech.Infrastructure.Context;
using AmbevTech.Infrastructure.EventBus;
using AmbevTech.Infrastructure.Publisher;
using AmbevTech.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Tingle.EventBus;

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

        //services.AddEventBus(builder =>
        //{
        //    builder.AddRabbitMqTransport(options =>
        //    {
        //        options.Hostname= "";
        //        options.Username = "";
        //        options.Password = "";
        //    });
        //});

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vendas API", Version = "v1" });
        });

        services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
        services.AddScoped<IEventBus, EventBusRabbitMQ>();

        builder.Services.AddControllers();
        builder.Services.AddSerilog();
        builder.Services.AddExceptionHandler<ErrorHandlingMiddleware>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Vendas API");
        });

        app.UseExceptionHandler(opt => { });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
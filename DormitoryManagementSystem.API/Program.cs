using DormitoryManagementSystem.API.Configuration.IServiceCollectionExtensions;
using DormitoryManagementSystem.API.Configuration.Logging;
using DormitoryManagementSystem.API.Middlewares;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Serilog;

namespace DormitoryManagementSystem.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = LogConfigurator.InitializeLogger();
        Log.Information("Starting up REST API.");

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSerilog();

        builder.AddServices();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseDomainEventsPublisher();

        app.MapControllers();

        await app.Services.GetRequiredService<IDomainEventSubscriber>().SubscribeToAllEvents();

        app.Run();
    }
}

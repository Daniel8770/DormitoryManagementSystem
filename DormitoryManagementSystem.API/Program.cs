using DormitoryManagementSystem.API.Configuration.IServiceCollectionExtensions;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;

namespace DormitoryManagementSystem.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen();

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

        await app.Services.GetRequiredService<IDomainEventSubscriber>().SubscribeToAllEvents();

        app.Run();
    }
}

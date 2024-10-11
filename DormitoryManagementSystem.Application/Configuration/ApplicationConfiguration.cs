using DormitoryManagementSystem.Application.KitchenContext;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;


namespace DormitoryManagementSystem.Application.Configuration;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AutoRegisterHandlersFromAssemblyOf<KitchenAccountCreatedHandler>();
        services.AddScoped<KitchenService>();
        return services;
    }
}

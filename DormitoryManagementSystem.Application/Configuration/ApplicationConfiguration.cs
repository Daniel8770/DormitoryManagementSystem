using DormitoryManagementSystem.Application.Clubs;
using DormitoryManagementSystem.Application.KitchenContext;
using DormitoryManagementSystem.Application.KitchenContext.Economy;
using DormitoryManagementSystem.Application.NotificationContext.Handlers;
using DormitoryManagementSystem.Application.SharedExpensesContext;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;


namespace DormitoryManagementSystem.Application.Configuration;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<KitchenService>();
        services.AddScoped<BookableResourceService>();
        services.AutoRegisterHandlersFromAssemblyOf<KitchenAccountCreatedEventHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<ResourceBookedEventHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<NotifyResourceBookedMessageHandler>();
        return services;
    }
}

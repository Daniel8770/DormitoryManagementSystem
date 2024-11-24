using DormitoryManagementSystem.Application.AccountingContext;
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
        services.AddScoped<IKitchenService, KitchenService>();
        services.AddScoped<IBookableResourceService, BookableResourceService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AutoRegisterHandlersFromAssemblyOf<KitchenAccountCreatedEventHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<ResourceBookedEventHandler>();
        services.AutoRegisterHandlersFromAssemblyOf<NotifyResourceBookedMessageHandler>();
        return services;
    }
}

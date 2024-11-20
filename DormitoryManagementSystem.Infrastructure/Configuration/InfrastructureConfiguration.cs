using DormitoryManagementSystem.Infrastructure.Configuration.Options;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;
using Rebus.Retry.Simple;
using Microsoft.Extensions.Configuration;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using DormitoryManagementSystem.Infrastructure.KitchenContext;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Infrastructure.KitchenContext.Economy;
using DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
using DormitoryManagementSystem.Infrastructure.SharedExpensesContext;
using DormitoryManagementSystem.Domain.AccountingContext.DomainEvents;
using DormitoryManagementSystem.Domain.SharedExpensesContext.IntegrationMessages;
using DormitoryManagementSystem.Infrastructure.ClubsContext;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Configuration;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection infraStructureConfig)
    {
        services.AddSingleton<DomainEventPublisher, RebusDomainEventPublisher>();
        services.AddSingleton<IDomainEventSubscriber, RebusDomainEventSubscriber>();

        services.Configure<RebusOptions>(infraStructureConfig.GetRequiredSection(RebusOptions.SectionName));
        services.ConfigureRebus(GetOptions<RebusOptions>(infraStructureConfig, RebusOptions.SectionName));

        services.ConfigureEntityFramework(infraStructureConfig.GetConnectionString("Default") 
            ?? throw new Exception("Could not load default connectionstring from appsettigns."));

        services.AddInMemoryRepositories();

        return services;
    }

    public static IServiceCollection ConfigureRebus(this IServiceCollection services, RebusOptions options)
    {
        services.AddRebus(configure =>
        {
            configure.Serialization(s => s.UseNewtonsoftJson());
            configure.Transport(t => t.UseSqlServer(new SqlServerTransportOptions(
                options.ConnectionString), 
                options.InputQueue)
            );
            configure.Subscriptions(s => s.StoreInSqlServer(options.ConnectionString, options.SubscriptionTable));
            configure.Routing(r => 
                r.TypeBased().MapAssemblyNamespaceOf<KitchenAccountCreatedEvent>(options.InputQueue)
                .MapAssemblyNamespaceOf<CreateSharedExpenseBalancerMessage>(options.InputQueue)
                .MapAssemblyNamespaceOf<DisposableAmountLowerLimitBreachedEvent>(options.InputQueue)
                .MapAssemblyNamespaceOf<SharedExpenseBalancerCreatedMessage>(options.InputQueue)
            );
            configure.Options(o =>
            {
                o.RetryStrategy(errorQueueName: options.ErrorQueue, maxDeliveryAttempts: options.MaxDeliveryAttempts);
                o.SetNumberOfWorkers(options.NumberOfWorkers);
                o.SetMaxParallelism(options.MaxParallelism);
            });
            return configure;
        });

        return services;
    }

    public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, string connectionstring)
    {
        services.AddDbContext<ClubsDBContext>(options =>
            options.UseSqlServer(connectionstring)
        );
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IKitchenRepository, InMemoryKitchenRepository>();
        services.AddSingleton<IKitchenBalanceRepository, InMemoryKitchenBalanceRepository>();
        services.AddSingleton<ISharedExpensesBalancerRepository, InMemorySharedExpensesBalancerRepository>();
        return services;
    }

    private static T GetOptions<T>(IConfigurationSection infraStructureConfig, string sectionName)
    {
        return infraStructureConfig
            .GetRequiredSection(sectionName)
            .Get<T>() ??
            throw new InvalidOperationException($"Section '{sectionName}' was not found in configuration.");
    }

}

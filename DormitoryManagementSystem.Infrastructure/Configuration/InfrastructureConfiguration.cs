using DormitoryManagementSystem.Infrastructure.Configuration.Options;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;
using Rebus.Retry.Simple;
using Microsoft.Extensions.Configuration;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;

namespace DormitoryManagementSystem.Infrastructure.Configuration;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection infraStructureConfig)
    {
        services.AddScoped<DomainEventPublisher, RebusDomainEventPublisher>();
        services.AddSingleton<IDomainEventSubscriber, RebusDomainEventSubscriber>();

        services.Configure<RebusOptions>(infraStructureConfig.GetRequiredSection(RebusOptions.SectionName));
        services.ConfigureRebus(GetOptions<RebusOptions>(infraStructureConfig, RebusOptions.SectionName));

        services.ConfigureEntityFrameworkd();

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
            configure.Routing(r => r.TypeBased().MapAssemblyNamespaceOf<KitchenAccountCreated>(options.InputQueue));
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

    public static IServiceCollection ConfigureEntityFrameworkd(this IServiceCollection services)
    {
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

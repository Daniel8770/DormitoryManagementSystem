﻿using DormitoryManagementSystem.Infrastructure.Configuration.Options;
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
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Rebus.Config.Outbox;
using DormitoryManagementSystem.Domain.ClubsContext.DomainEvents;
using Rebus.Bus;
using DormitoryManagementSystem.Infrastructure.Common.Persistence;
using DormitoryManagementSystem.Domain.ClubsContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Infrastructure.AccountingContext;

namespace DormitoryManagementSystem.Infrastructure.Configuration;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection infrastructureConfig)
    {
        services.AddScoped<IDomainEventPublisher, RebusDomainEventPublisher>();
        services.AddSingleton<IDomainEventSubscriber, RebusDomainEventSubscriber>();

        services.Configure<RebusOptions>(infrastructureConfig.GetRequiredSection(RebusOptions.SectionName));
        services.ConfigureRebus(GetOptions<RebusOptions>(infrastructureConfig, RebusOptions.SectionName));

        services.AddRepositories(infrastructureConfig);

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
                .MapAssemblyNamespaceOf<ResourceBookedEvent>(options.InputQueue)
                .MapAssemblyNamespaceOf<NotifyResourceBookedMessage>(options.InputQueue)
            );
            configure.Outbox(o => o.StoreInSqlServer(options.ConnectionString, options.OutboxTable));
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

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfigurationSection infrastructureConfig)
    {
        string connectionstring = infrastructureConfig.GetConnectionString("Dapper")
            ?? throw new Exception("Could not load default connectionstring from appsettigns.");

        services.AddScoped<DBConnection>(serviceProvider =>
            new DBConnection(connectionstring, serviceProvider.GetRequiredService<IDomainEventPublisher>())
        );

        services.AddScoped<IBookableResourceRepository, DapperBookableResourceRepository>(serviceProvider =>
            new DapperBookableResourceRepository(
                connectionstring, 
                serviceProvider.GetRequiredService<DBConnection>())
        );

        services.AddSingleton<IKitchenRepository, InMemoryKitchenRepository>();
        services.AddSingleton<IKitchenBalanceRepository, InMemoryKitchenBalanceRepository>();
        services.AddSingleton<ISharedExpensesBalancerRepository, InMemorySharedExpensesBalancerRepository>();
        services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();

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

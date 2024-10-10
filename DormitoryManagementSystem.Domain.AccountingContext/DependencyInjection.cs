using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace DormitoryManagementSystem.Domain.AccountingContext;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountingContext(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository>();
        return services;
    }
}

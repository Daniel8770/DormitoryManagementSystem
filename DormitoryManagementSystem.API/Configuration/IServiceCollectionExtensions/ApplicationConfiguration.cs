using DormitoryManagementSystem.Infrastructure.Configuration;
using DormitoryManagementSystem.Application.Configuration;

namespace DormitoryManagementSystem.API.Configuration.IServiceCollectionExtensions;

public static class ApplicationConfiguration
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Configuration.GetRequiredSection("Infrastructure"));
        builder.Services.AddApplication();
        return builder;
    }
}

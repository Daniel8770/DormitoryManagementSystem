using Serilog;
using Serilog.Events;
using System.Reflection;

namespace DormitoryManagementSystem.API.Configuration.Logging;

public class LogConfigurator
{
    public static Serilog.ILogger InitializeLogger()
    {
        //string path = Path.Combine(Directory.GetCurrentDirectory(), "/Logs/log-.txt");
        string path = "Logs/log-.txt";

        return new LoggerConfiguration()
            .MinimumLevel.Information()
            //.MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .Enrich.With(new LogEnricher())
            .WriteTo.Console()
            .WriteTo.File(
                path,
                rollingInterval: RollingInterval.Month,
                outputTemplate: "[{Level:u3}] {Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{ThreadID}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }
}

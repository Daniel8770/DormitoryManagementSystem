using Serilog.Core;
using Serilog.Events;

namespace DormitoryManagementSystem.API.Configuration.Logging;

public class LogEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
            "ThreadID", Thread.CurrentThread.ManagedThreadId.ToString("D3")));

        //logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
        //    "ClassName", GetType().Name));

        //logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
        //    "MethodName", ));
    }
}

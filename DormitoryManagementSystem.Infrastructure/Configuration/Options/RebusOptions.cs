
namespace DormitoryManagementSystem.Infrastructure.Configuration.Options;
public class RebusOptions
{
    public const string SectionName = "Rebus";
    public int MaxDeliveryAttempts { get; set; }
    public int MaxRetries { get; set; }
    public int RetryLatencySeconds { get; set; }
    public int NumberOfWorkers { get; set; }
    public int MaxParallelism { get; set; }
    public string InputQueue { get; set; } = string.Empty;
    public string ErrorQueue { get; set; } = string.Empty;
    public string OutboxTable { get; set; } = string.Empty;
    public string SubscriptionTable { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
}
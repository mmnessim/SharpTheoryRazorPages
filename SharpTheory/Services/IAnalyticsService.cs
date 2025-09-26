namespace SharpTheory.Services
{
    public interface IAnalyticsService
    {
        Task SendEventAsync(string eventType, object? payload = null, string? userId = null);
    }
}

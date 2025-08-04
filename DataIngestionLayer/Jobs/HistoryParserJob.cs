using DataIngestionLayer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataIngestionLayer.Jobs;
public class HistoryParserJob : IHostedService, IDisposable
{
    private readonly ILogger<HistoryParserJob> _logger;
    private readonly IBrowserHistoryParser _browserHistoryParser;
    private Timer? _timer;

    public HistoryParserJob(
        ILogger<HistoryParserJob> logger,
        IBrowserHistoryParser browserHistoryParser)
    {
        _logger = logger;
        _browserHistoryParser = browserHistoryParser;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting the service...");
        var entries = _browserHistoryParser.ParseHistory();
        // Implement the logic to parse firefox history here

        // Setup timer for DoWork execution every hour
        _timer = new Timer(async (state) => await DoWork(), null, TimeSpan.Zero, TimeSpan.FromHours(1));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping the service...");

        // Stop the timer
        if (_timer is not null)
            _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async Task DoWork()
    {
        try
        {
            var entries = _browserHistoryParser.ParseHistory();
            // Implement the logic to parse firefox history here
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during DoWork execution");
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

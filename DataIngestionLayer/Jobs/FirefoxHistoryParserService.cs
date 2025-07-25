using DataIngestionLayer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataIngestionLayer.Jobs;

public class HistoryParserJob : IHostedService
{
    private readonly ILogger<HistoryParserJob> _logger;
    private readonly IBrowserHistoryParser _browserHistoryParser;

    public HistoryParserJob(
        ILogger<HistoryParserJob> logger,
        IBrowserHistoryParser browserHistoryParser)
    {
        _logger = logger;
        _browserHistoryParser = browserHistoryParser;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting the service...");
        _browserHistoryParser.ParseHistory();
        // Implement the logic to parse firefox history here
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping the service...");
    }
}
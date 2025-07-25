using DataIngestionLayer.Options;
using DataIngestionLayer.Services;
using DataIngestionLayer.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngestionLayer;

public static class ConfigureServices
{
    // Register services for FirefoxHistoryParser
    public static void AddFirefoxHistoryParser(this IServiceCollection services)
    {
        services.AddScoped<IBrowserHistoryParser, FirefoxHistoryParser>();
        services.AddOptions<FirefoxSettings>();
    }


}
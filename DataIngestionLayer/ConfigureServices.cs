using DataIngestionLayer.Jobs;
using DataIngestionLayer.Options;
using DataIngestionLayer.Services;
using DataIngestionLayer.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngestionLayer;

public static class ConfigureServices
{
    // Register services for FirefoxHistoryParser
    public static IServiceCollection AddFirefoxHistoryParser(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<FirefoxSettings>()
                .Bind(config.GetSection(FirefoxSettings.Key));

        services.AddHostedService<HistoryParserJob>();

        services.AddScoped<IBrowserHistoryParser, FirefoxHistoryParser>();

        return services;
    }


}
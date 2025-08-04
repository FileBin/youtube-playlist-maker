using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using DataIngestionLayer;
using Microsoft.Extensions.Configuration;


var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddFirefoxHistoryParser(configuration);
    })
    .ConfigureLogging((context, loggingBuilder) =>
    {
        loggingBuilder.AddConsole();
        loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
    })
    .Build();

await host.RunAsync();

using System;
using System.Collections.Generic;
using System.Linq;
using BrowserHistoryParser.Jobs;
using BrowserHistoryParser.Services;
using BrowserHistoryParser.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IBrowserHistoryParser, FirefoxHistoryParser>();
        services.AddHostedService<HistoryParserJob>(); // Implement the hosted service
    })
    .ConfigureLogging((context, loggingBuilder) =>
    {
        loggingBuilder.AddConsole();
        loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
    })
    .Build();

await host.RunAsync();

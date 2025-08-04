using System.Data;
using System.Text.RegularExpressions;
using DataIngestionLayer.Options;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataIngestionLayer.Services.Implementation;

public partial class FirefoxHistoryParser(
    IOptions<FirefoxSettings> settings,
    ILogger<FirefoxHistoryParser> logger) : IBrowserHistoryParser
{
    public IReadOnlyCollection<Uri> ParseHistory()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(home, ".mozilla", "firefox", settings.Value.ProfileName, "places.sqlite");

        var tempFile = Path.GetTempFileName();
        
        using (var sourceStream = new FileStream(filePath, FileMode.Open))
        {
            using var destinationStream = new FileStream(tempFile, FileMode.Truncate);
            sourceStream.CopyTo(destinationStream);
        }

        using var connection = new SqliteConnection($"Data Source={tempFile}");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT url FROM moz_places";
        var reader = command.ExecuteReader();
        var historyUrls = new List<Uri>();
        while (reader.Read())
        {
            var url = reader["url"].ToString();
            if (url is null)
                continue;

            var match = YoutubeLinkRegex().Match(url);

            if (!match.Success)
                continue;

            var id = match.Groups["id"];
            var list = match.Groups["list"];

            logger.Log(LogLevel.Information, "Parsed youtube video id {id} in list {list}", id, list);
            historyUrls.Add(new Uri(url));
        }
        reader.Close();
        return historyUrls;
    }

    [GeneratedRegex(@"https:\/\/www\.youtube\.com\/watch\?v=(?<id>[a-zA-Z0-9_-]+)\&list=(?<list>[a-zA-Z0-9_-]+)")]
    private static partial Regex YoutubeLinkRegex();
}
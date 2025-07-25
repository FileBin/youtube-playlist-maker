using System.Data;
using System.Text.RegularExpressions;
using DataIngestionLayer.Options;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace DataIngestionLayer.Services.Implementation;

public partial class FirefoxHistoryParser(IOptions<FirefoxSettings> settings) : IBrowserHistoryParser
{
    private readonly string _profileFolder = settings.Value.ProfileName;

    public IReadOnlyCollection<Uri> ParseHistory()
    {
        // Load history file (assuming it's in the correct location)
        string filePath = Path.Combine("~", "mozilla", "firefox", _profileFolder, "places.sqlite");
        using var connection = new SqliteConnection("Data Source=" + filePath);
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
                
            historyUrls.Add(new Uri(url));
        }
        reader.Close();
        return historyUrls;
    }

    [GeneratedRegex(@"https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+")]
    private static partial Regex YoutubeLinkRegex();
}
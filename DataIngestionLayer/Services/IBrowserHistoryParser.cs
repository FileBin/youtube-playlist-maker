namespace DataIngestionLayer.Services;

// Remove placeholder and start writing from here

public interface IBrowserHistoryParser
{
    IReadOnlyCollection<Uri> ParseHistory();
}

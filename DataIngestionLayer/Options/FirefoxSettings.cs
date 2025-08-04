namespace DataIngestionLayer.Options;

public class FirefoxSettings
{
    public const string Key = nameof(FirefoxSettings);

    public string ProfileName { get; set; } = "default";
}

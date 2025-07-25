using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace DataIngestionLayer.Options;

public class FirefoxSettings
{
    public string ProfileName { get; set; } = "default";
}

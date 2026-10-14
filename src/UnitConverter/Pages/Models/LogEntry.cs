using System.Text.Json.Serialization;

namespace UnitConverter.Models;
public class LogEntry
{
[JsonPropertyName("@t")]
public DateTimeOffset Timestamp { get; set; }

[JsonPropertyName("@m")]
public string Message { get; set; }

[JsonPropertyName("@l")]
public string? Level { get; set; }

[JsonPropertyName("@x")]
public string? Exception { get; set; }

public dynamic Input {get; set;}
public String? ConversionType {get; set;}
public decimal? Result {get; set;}
}

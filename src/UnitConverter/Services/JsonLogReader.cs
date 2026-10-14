using System.Text.Json;
using UnitConverter.Models;

namespace UnitConverter.Services;

public class JsonLogReader : ILogReader
{
    private readonly string _logDirectory;

    public JsonLogReader(IWebHostEnvironment environment)
    {
        _logDirectory = Path.Combine(
            environment.ContentRootPath,
            "Logs");
    }

    public IEnumerable<LogEntry> Read()
    {
        var entries = new List<LogEntry>();

        if (!Directory.Exists(_logDirectory))
        {
            return entries;
        }

        string[] logFiles = Directory.GetFiles(_logDirectory, "*.json");

        foreach (string logFile in logFiles)
        {
            string[] lines = ReadAllLinesShared(logFile);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                LogEntry? entry;
                try
                {
                    entry = JsonSerializer.Deserialize<LogEntry>(line);
                }
                catch (JsonException)
                {
                    continue;
                }

                if (entry is null)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(entry.Level))
                {
                    entry.Level = "Information";
                }

                entries.Add(entry);
            }
        }

        return entries;
    }

    private static string[] ReadAllLinesShared(string path)
    {
        using var stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite);
        using var reader = new StreamReader(stream);

        var lines = new List<string>();
        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            lines.Add(line);
        }

        return lines.ToArray();
    }
}

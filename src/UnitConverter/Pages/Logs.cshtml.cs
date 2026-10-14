using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class LogsModel : PageModel
{
    private readonly ILogReader _logReader;

    public LogsModel(ILogReader logReader)
    {
        _logReader = logReader;
    }

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Level { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    public List<LogEntry> Entries { get; set; } = new();

    public void OnGet()
    {
        IEnumerable<LogEntry> allEntries = _logReader.Read();
        var filtered = new List<LogEntry>();

        foreach (LogEntry entry in allEntries)
        {
            if (!string.IsNullOrWhiteSpace(Level) &&
                !string.Equals(entry.Level, Level, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(ConversionType) &&
                !string.Equals(entry.ConversionType, ConversionType, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!string.IsNullOrWhiteSpace(Search))
            {
                string message = entry.Message ?? string.Empty;
                if (!message.Contains(Search, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }

            filtered.Add(entry);
        }

        Entries = filtered;
    }
}

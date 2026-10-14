using Serilog;
using UnitConverter.Models;
namespace UnitConverter.Services;

public interface ILogReader
{
    IEnumerable<LogEntry> Read();
}

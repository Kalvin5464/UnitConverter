using Serilog;
using Serilog.Formatting.Compact;
using UnitConverter.Services;

var builder = WebApplication.CreateBuilder(args);

string logPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Logs",
    "unitconverter-.json");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        new RenderedCompactJsonFormatter(),
        logPath,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IConversionService, UnitOfConversionService>();
builder.Services.AddSingleton<ILogReader, JsonLogReader>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

try
{
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class QuickConversionsModel : PageModel
{
    private readonly IConversionService _conversionService;
    private readonly ILogger<QuickConversionsModel> _logger;

    public QuickConversionsModel(
        IConversionService conversionService,
        ILogger<QuickConversionsModel> logger)
    {
        _conversionService = conversionService;
        _logger = logger;
    }

    public string Output { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    public IEnumerable<SelectListItem> PoundOptions =>
    [
        new("1 pound", "1"),
        new("5 pounds", "5"),
        new("10 pounds", "10"),
        new("25 pounds", "25"),
        new("50 pounds", "50")
    ];

    public IActionResult OnGetMilesToKilometers(string input)
    {
        return PerformConversion(input, ConversionTypes.MilesToKilometers);
    }

    public IActionResult OnGetKilometersToMiles(string input)
    {
        return PerformConversion(input, ConversionTypes.KilometersToMiles);
    }

    public IActionResult OnGetFahrenheitToCelsius(string input)
    {
        return PerformConversion(input, ConversionTypes.FahrenheitToCelsius);
    }

    public IActionResult OnGetCelsiusToFahrenheit(string input)
    {
        return PerformConversion(input, ConversionTypes.CelsiusToFahrenheit);
    }

    public IActionResult OnGetPoundsToKilograms(string input)
    {
        return PerformConversion(input, ConversionTypes.PoundsToKilograms);
    }

    public IActionResult OnGetKilogramsToPounds(string input)
    {
        return PerformConversion(input, ConversionTypes.KilogramsToPounds);
    }

    public IActionResult OnGetFeetToMeters(string input)
    {
        return PerformConversion(input, ConversionTypes.FeetToMeters);
    }

    public IActionResult OnGetMetersToFeet(string input)
    {
        return PerformConversion(input, ConversionTypes.MetersToFeet);
    }

    private IActionResult PerformConversion(string input, string conversionType)
    {
        _logger.LogInformation(
            "Conversion request received for {ConversionType} with input {Input}",
            conversionType,
            input);

        decimal value;
        try
        {
            value = Convert.ToDecimal(input);
        }
        catch (FormatException)
        {
            _logger.LogWarning(
                "Invalid conversion input {Input} for {ConversionType}",
                input,
                conversionType);
            ErrorMessage = "Input must be a valid number.";
            return Page();
        }
        catch (OverflowException)
        {
            _logger.LogWarning(
                "Invalid conversion input {Input} for {ConversionType}",
                input,
                conversionType);
            ErrorMessage = "Input is too large or too small.";
            return Page();
        }

        try
        {
            decimal result = _conversionService.Convert(value, conversionType);
            Output = result.ToString();

            _logger.LogInformation(
                "Converted {Input} using {ConversionType} with result {Result}",
                value,
                conversionType,
                result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(
                ex,
                "Conversion failed for {ConversionType} with input {Input}",
                conversionType,
                input);
            ErrorMessage = "Unknown conversion type.";
        }

        return Page();
    }
}

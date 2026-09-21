using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitOf;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)] public string Input { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;


    public void OnGet()
    {
        ViewData["Title"] = "Conversions";
        ViewData["ConversionType"] = ConversionType;

        double value;
        try
        {
            value = Convert.ToDouble(Input);
        }

        catch (FormatException)

        {
            ViewData["ErrorMessage"] = "Entered Characters must be a number";
            return;
        }

        catch (OverflowException)

        {
            ViewData["ErrorMessage"] = "Entered Characters must be a number";
            return;
        }


        double? result;
        try

        {
            result = ConversionType switch

            {
                "MilesToKilometers" => new Length().FromMiles(value).ToKilometers(),
                "KilometersToMiles" => new Length().FromKilometers(value).ToMiles(),
                "FahrenheitToCelsius" => new Temperature().FromFahrenheit(value).ToCelsius(),
                "CelsiusToFahrenheit" => new Temperature().FromCelsius(value).ToFahrenheit(),
                "PoundsToKilograms" => new Mass().FromPounds(value).ToKilograms(),
                "KilogramsToPounds" => new Mass().FromKilograms(value).ToPounds(),
                "FeetToMeters" => new Length().FromFeet(value).ToMeters(),
                "MetersToFeet" => new Length().FromMeters(value).ToFeet(),
                _=> null
            };
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] = "Conversion was unable to be completed";
            return;
        }

        if (result is null)
        {
            ViewData["ErrorMessage"] = "Unknown Conversion type error";
            return;
        }
        Output = result.Value.ToString();
        //     Input = "3.1415";
        //     ViewData["ConversionType"] = "Miles to Kilometers";
        //     ViewData["Title"] = "Conversions";
        //     double Miles = Convert.ToDouble(Input);
        //     double Kilometers = new Length().FromMiles(Miles).ToKilometers();
        //     Output = Kilometers.ToString();
        //


    }
}

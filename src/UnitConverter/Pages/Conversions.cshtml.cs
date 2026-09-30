using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitOf;

namespace UnitConverter.Pages;


public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string ConversionType
    {
        get => Conversion.ConversionType;
        set => Conversion.ConversionType = value;
        }

    [BindProperty(SupportsGet = true)]
    public string Input
    {
        get => Conversion.Input;
        set => Conversion.Input = value;
    }

    public string Output
    {
        get => Conversion.Output;
        set => Conversion.Output = value;
    }

    public void OnGet()
    {
        ViewData["Title"] = "Conversions";

        if (string.IsNullOrWhiteSpace(Conversion.ConversionType) && string.IsNullOrWhiteSpace(Conversion.Input))
        {
            Conversion.ConversionType = ConversionTypes.MilesToKilometers;
            Conversion.Input = "3.1415";
            ViewData["ConversionType"] = ConversionTypes.All[ConversionTypes.MilesToKilometers];
        }
        else
        {
            ViewData["ConversionType"] =  Conversion.ConversionType;
        }

        double value;
        try
        {
            value = Convert.ToDouble(Conversion.Input);
        }

        catch (FormatException)

        {
            ViewData["ErrorMessage"] = "Entered Characters must be a number";
            return;
        }

        catch (OverflowException)

        {
            ViewData["ErrorMessage"] = "Too large, way too small";
            return;
        }


        double? result;
        try

        {
            result = Conversion.ConversionType switch

            {
                ConversionTypes.MilesToKilometers => new Length().FromMiles(value).ToKilometers(),
                ConversionTypes.KilometersToMiles => new Length().FromKilometers(value).ToMiles(),
                ConversionTypes.FahrenheitToCelsius => new Temperature().FromFahrenheit(value).ToCelsius(),
                ConversionTypes.CelsiusToFahrenheit => new Temperature().FromCelsius(value).ToFahrenheit(),
                ConversionTypes.PoundsToKilograms => new Mass().FromPounds(value).ToKilograms(),
                ConversionTypes.KilogramsToPounds => new Mass().FromKilograms(value).ToPounds(),
                ConversionTypes.FeetToMeters => new Length().FromFeet(value).ToMeters(),
                ConversionTypes.MetersToFeet => new Length().FromMeters(value).ToFeet(),
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
        Conversion.Output = result.Value.ToString();



    }
}

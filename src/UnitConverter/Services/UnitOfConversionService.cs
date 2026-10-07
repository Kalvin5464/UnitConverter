namespace UnitConverter.Services;
using UnitConverter.Models;
using UnitOf;

public class UnitOfConversionService : IConversionService
{
    public decimal Convert(decimal value, string conversionType)
    {

        double doubleValue = (double)value;

        double? result = conversionType switch
        {
            ConversionTypes.MilesToKilometers => new Length().FromMiles(doubleValue).ToKilometers(),
            ConversionTypes.KilometersToMiles => new Length().FromKilometers(doubleValue).ToMiles(),
            ConversionTypes.FahrenheitToCelsius => new Temperature().FromFahrenheit(doubleValue).ToCelsius(),
            ConversionTypes.CelsiusToFahrenheit => new Temperature().FromCelsius(doubleValue).ToFahrenheit(),
            ConversionTypes.PoundsToKilograms => new Mass().FromPounds(doubleValue).ToKilograms(),
            ConversionTypes.KilogramsToPounds => new Mass().FromKilograms(doubleValue).ToPounds(),
            ConversionTypes.FeetToMeters => new Length().FromFeet(doubleValue).ToMeters(),
            ConversionTypes.MetersToFeet => new Length().FromMeters(doubleValue).ToFeet(),
            _ => null
        };

        if (result is null)
        {
            throw new ArgumentException($"Unkown conversion type: {conversionType}");

        }

        return (decimal)result.Value;
    }
}

namespace UnitConverter.Pages;

public static class ConversionTypes
{
    public const string MilesToKilometers = "MilesToKilometers";
    public const string KilometersToMiles = "KilometersToMiles";
    public const string FahrenheitToCelsius = "FahrenheitToCelsius";
    public const string CelsiusToFahrenheit = "CelsiusToFahrenheit";
    public const string PoundsToKilograms = "PoundsToKilograms";
    public const string KilogramsToPounds = "KilogramsToPounds";
    public const string FeetToMeters = "FeetToMeters";
    public const string MetersToFeet = "MetersToFeet";

    public static readonly IReadOnlyDictionary<string, string> All =
        new Dictionary<string, string>
        {
            [MilesToKilometers] = "Miles to Kilometers",
            [KilometersToMiles] = "Kilometers to Miles",
            [FahrenheitToCelsius] = "Fahrenheit To Celsius",
            [CelsiusToFahrenheit] = "Celsius To Fahrenheit",
            [PoundsToKilograms] = "Pounds To Kilograms",
            [KilogramsToPounds] = "Kilograms To Pounds",
            [FeetToMeters] = "Feet To Meters",
            [MetersToFeet] = "Meters To Feet"
        };
}

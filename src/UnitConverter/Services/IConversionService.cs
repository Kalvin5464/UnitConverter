namespace UnitConverter.Services;

public interface IConversionService
{
    decimal Convert(decimal value, string ConversionType);
}

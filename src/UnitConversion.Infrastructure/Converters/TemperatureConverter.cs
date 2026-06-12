using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Converters;

public class TemperatureConverter : IUnitConverter
{
    private readonly HashSet<string> _supportedUnits = new(StringComparer.OrdinalIgnoreCase)
    {
        "celsius", "c", "fahrenheit", "f", "kelvin", "k"
    };

    public ConversionCategory Category => ConversionCategory.Temperature;

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return _supportedUnits.Contains(NormalizeUnit(fromUnit)) &&
               _supportedUnits.Contains(NormalizeUnit(toUnit));
    }

    public double Convert(double value, string fromUnit, string toUnit)
    {
        var from = NormalizeUnit(fromUnit);
        var to = NormalizeUnit(toUnit);

        if (from == to) return value;

        double celsius = from switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32) * 5.0 / 9.0,
            "kelvin" => value - 273.15,
            _ => throw new ArgumentException($"Unknown temperature unit: {fromUnit}")
        };

        double result = to switch
        {
            "celsius" => celsius,
            "fahrenheit" => (celsius * 9.0 / 5.0) + 32,
            "kelvin" => celsius + 273.15,
            _ => throw new ArgumentException($"Unknown temperature unit: {toUnit}")
        };

        return result;
    }

    private static string NormalizeUnit(string unit) => unit.ToLowerInvariant() switch
    {
        "c" => "celsius",
        "f" => "fahrenheit",
        "k" => "kelvin",
        var u => u
    };
}

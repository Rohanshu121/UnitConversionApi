using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Converters;

public class LengthConverter : IUnitConverter
{
    private readonly Dictionary<string, double> _toMeterFactor = new(StringComparer.OrdinalIgnoreCase)
    {
        ["meter"] = 1.0,
        ["kilometer"] = 1000.0,
        ["centimeter"] = 0.01,
        ["millimeter"] = 0.001,
        ["inch"] = 0.0254,
        ["foot"] = 0.3048,
        ["yard"] = 0.9144,
        ["mile"] = 1609.344
    };

    public ConversionCategory Category => ConversionCategory.Length;

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return _toMeterFactor.ContainsKey(fromUnit) && _toMeterFactor.ContainsKey(toUnit);
    }

    public double Convert(double value, string fromUnit, string toUnit)
    {
        if (!CanConvert(fromUnit, toUnit))
            throw new ArgumentException($"Invalid length units: {fromUnit} -> {toUnit}");

        double meters = value * _toMeterFactor[fromUnit];
        return meters / _toMeterFactor[toUnit];
    }
}

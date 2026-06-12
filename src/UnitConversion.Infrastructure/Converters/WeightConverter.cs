using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Enums;

namespace UnitConversion.Infrastructure.Converters;

public class WeightConverter : IUnitConverter
{
    private readonly Dictionary<string, double> _toGramFactor = new(StringComparer.OrdinalIgnoreCase)
    {
        ["gram"] = 1.0,
        ["kilogram"] = 1000.0,
        ["pound"] = 453.59237,
        ["ounce"] = 28.349523125
    };

    public ConversionCategory Category => ConversionCategory.Weight;

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return _toGramFactor.ContainsKey(fromUnit) && _toGramFactor.ContainsKey(toUnit);
    }

    public double Convert(double value, string fromUnit, string toUnit)
    {
        if (!CanConvert(fromUnit, toUnit))
            throw new ArgumentException($"Invalid weight units: {fromUnit} -> {toUnit}");

        double grams = value * _toGramFactor[fromUnit];
        return grams / _toGramFactor[toUnit];
    }
}

using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Models;

namespace UnitConversion.Infrastructure.Registry;

public static class UnitRegistry
{
    public static IReadOnlyDictionary<ConversionCategory, IReadOnlyList<UnitDefinition>> GetAllUnits()
    {
        return new Dictionary<ConversionCategory, IReadOnlyList<UnitDefinition>>
        {
            [ConversionCategory.Length] = new List<UnitDefinition>
            {
                new() { Name = "Meter", Symbol = "m", ToBaseFactor = 1.0 },
                new() { Name = "Kilometer", Symbol = "km", ToBaseFactor = 1000.0 },
                new() { Name = "Centimeter", Symbol = "cm", ToBaseFactor = 0.01 },
                new() { Name = "Millimeter", Symbol = "mm", ToBaseFactor = 0.001 },
                new() { Name = "Inch", Symbol = "in", ToBaseFactor = 0.0254 },
                new() { Name = "Foot", Symbol = "ft", ToBaseFactor = 0.3048 },
                new() { Name = "Yard", Symbol = "yd", ToBaseFactor = 0.9144 },
                new() { Name = "Mile", Symbol = "mi", ToBaseFactor = 1609.344 }
            },
            [ConversionCategory.Weight] = new List<UnitDefinition>
            {
                new() { Name = "Gram", Symbol = "g", ToBaseFactor = 1.0 },
                new() { Name = "Kilogram", Symbol = "kg", ToBaseFactor = 1000.0 },
                new() { Name = "Pound", Symbol = "lb", ToBaseFactor = 453.59237 },
                new() { Name = "Ounce", Symbol = "oz", ToBaseFactor = 28.349523125 }
            },
            [ConversionCategory.Temperature] = new List<UnitDefinition>
            {
                new() { Name = "Celsius", Symbol = "°C" },
                new() { Name = "Fahrenheit", Symbol = "°F" },
                new() { Name = "Kelvin", Symbol = "K" }
            }
        };
    }
}

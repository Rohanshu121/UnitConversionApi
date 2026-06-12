using UnitConversion.Domain.Enums;

namespace UnitConversion.Application.Interfaces;

public interface IUnitConverter
{
    ConversionCategory Category { get; }
    bool CanConvert(string fromUnit, string toUnit);
    double Convert(double value, string fromUnit, string toUnit);
}

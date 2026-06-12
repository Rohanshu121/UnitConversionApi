using UnitConversion.Infrastructure.Converters;
using Xunit;

namespace UnitConversion.Tests;

public class LengthConverterTests
{
    private readonly LengthConverter _converter = new();

    [Fact]
    public void Meter_To_Foot_ShouldBeCorrect()
    {
        double result = _converter.Convert(10, "meter", "foot");
        Assert.Equal(32.8084, result, 4);
    }

    [Fact]
    public void Kilometer_To_Mile_ShouldBeCorrect()
    {
        double result = _converter.Convert(5, "kilometer", "mile");
        Assert.Equal(3.10686, result, 4);
    }

    [Fact]
    public void Inch_To_Centimeter_ShouldBeCorrect()
    {
        double result = _converter.Convert(1, "inch", "centimeter");
        Assert.Equal(2.54, result, 2);
    }
}
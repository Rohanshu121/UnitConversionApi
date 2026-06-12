using UnitConversion.Infrastructure.Converters;
using Xunit;

namespace UnitConversion.Tests;

public class TemperatureConverterTests
{
    private readonly TemperatureConverter _converter = new();

    [Fact]
    public void Celsius_To_Fahrenheit_ShouldBeCorrect()
    {
        double result = _converter.Convert(25, "celsius", "fahrenheit");
        Assert.Equal(77, result, 2);
    }

    [Fact]
    public void Fahrenheit_To_Celsius_ShouldBeCorrect()
    {
        double result = _converter.Convert(77, "fahrenheit", "celsius");
        Assert.Equal(25, result, 2);
    }

    [Fact]
    public void Celsius_To_Kelvin_ShouldBeCorrect()
    {
        double result = _converter.Convert(0, "celsius", "kelvin");
        Assert.Equal(273.15, result, 2);
    }

    [Fact]
    public void Kelvin_To_Celsius_ShouldBeCorrect()
    {
        double result = _converter.Convert(273.15, "kelvin", "celsius");
        Assert.Equal(0, result, 2);
    }
}
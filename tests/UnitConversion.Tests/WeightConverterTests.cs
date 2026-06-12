using UnitConversion.Infrastructure.Converters;
using Xunit;

namespace UnitConversion.Tests;

public class WeightConverterTests
{
    private readonly WeightConverter _converter = new();

    [Fact]
    public void Kilogram_To_Pound_ShouldBeCorrect()
    {
        double result = _converter.Convert(1, "kilogram", "pound");
        Assert.Equal(2.20462, result, 4);
    }

    [Fact]
    public void Gram_To_Ounce_ShouldBeCorrect()
    {
        double result = _converter.Convert(100, "gram", "ounce");
        Assert.Equal(3.5274, result, 4);
    }

    [Fact]
    public void Pound_To_Kilogram_ShouldBeCorrect()
    {
        double result = _converter.Convert(10, "pound", "kilogram");
        Assert.Equal(4.53592, result, 4);
    }
}
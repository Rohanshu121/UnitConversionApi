namespace UnitConversion.Domain.Models;

public class UnitDefinition
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public double ToBaseFactor { get; set; }
    public Func<double, double>? CustomToBase { get; set; }
    public Func<double, double>? CustomFromBase { get; set; }
}

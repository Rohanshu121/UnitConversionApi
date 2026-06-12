using System.Text.Json.Serialization;

namespace UnitConversion.Application.DTOs;

public class ConversionResponse
{
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("fromUnit")]
    public string FromUnit { get; set; } = string.Empty;

    [JsonPropertyName("toUnit")]
    public string ToUnit { get; set; } = string.Empty;

    [JsonPropertyName("originalValue")]
    public double OriginalValue { get; set; }

    [JsonPropertyName("convertedValue")]
    public double ConvertedValue { get; set; }
}

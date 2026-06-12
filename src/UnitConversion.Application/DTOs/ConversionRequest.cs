using System.Text.Json.Serialization;

namespace UnitConversion.Application.DTOs;

public class ConversionRequest
{
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("fromUnit")]
    public string FromUnit { get; set; } = string.Empty;

    [JsonPropertyName("toUnit")]
    public string ToUnit { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public double Value { get; set; }
}

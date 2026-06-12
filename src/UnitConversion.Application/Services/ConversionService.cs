using Microsoft.Extensions.Logging;
using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Exceptions;

namespace UnitConversion.Application.Services;

public class ConversionService : IConversionService
{
    private readonly IEnumerable<IUnitConverter> _converters;
    private readonly ILogger<ConversionService> _logger;

    public ConversionService(IEnumerable<IUnitConverter> converters, ILogger<ConversionService> logger)
    {
        _converters = converters;
        _logger = logger;
    }

    public Task<ConversionResponse> ConvertAsync(ConversionRequest request)
    {
        if (!Enum.TryParse<ConversionCategory>(request.Category, true, out var category))
        {
            throw new UnsupportedConversionException($"Unsupported conversion category: '{request.Category}'. Supported: Length, Weight, Temperature.");
        }

        var converter = _converters.FirstOrDefault(c => c.Category == category);
        if (converter == null)
        {
            throw new UnsupportedConversionException($"No converter registered for category '{request.Category}'.");
        }

        if (!converter.CanConvert(request.FromUnit, request.ToUnit))
        {
            throw new UnsupportedConversionException($"Cannot convert from '{request.FromUnit}' to '{request.ToUnit}' in category '{request.Category}'.");
        }

        try
        {
            var converted = converter.Convert(request.Value, request.FromUnit, request.ToUnit);
            return Task.FromResult(new ConversionResponse
            {
                Category = request.Category,
                FromUnit = request.FromUnit,
                ToUnit = request.ToUnit,
                OriginalValue = request.Value,
                ConvertedValue = converted
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Conversion error for {FromUnit} -> {ToUnit}", request.FromUnit, request.ToUnit);
            throw new UnsupportedConversionException($"Conversion failed: {ex.Message}");
        }
    }
}

using UnitConversion.Application.DTOs;

namespace UnitConversion.Application.Interfaces;

public interface IConversionService
{
    Task<ConversionResponse> ConvertAsync(ConversionRequest request);
}

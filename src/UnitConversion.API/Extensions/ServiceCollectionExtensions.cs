using UnitConversion.Application.Interfaces;
using UnitConversion.Application.Services;
using UnitConversion.Infrastructure.Converters;

namespace UnitConversion.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConversionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitConverter, LengthConverter>();
        services.AddScoped<IUnitConverter, WeightConverter>();
        services.AddScoped<IUnitConverter, TemperatureConverter>();
        services.AddScoped<IConversionService, ConversionService>();
        return services;
    }
}

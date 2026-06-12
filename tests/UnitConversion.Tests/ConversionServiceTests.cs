using Microsoft.Extensions.Logging;
using Moq;
using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;
using UnitConversion.Application.Services;
using UnitConversion.Domain.Exceptions;
using UnitConversion.Infrastructure.Converters;
using Xunit;

namespace UnitConversion.Tests;

public class ConversionServiceTests
{
    private readonly ConversionService _service;
    private readonly Mock<ILogger<ConversionService>> _loggerMock = new();

    public ConversionServiceTests()
    {
        var converters = new IUnitConverter[] { new LengthConverter(), new WeightConverter(), new TemperatureConverter() };
        _service = new ConversionService(converters, _loggerMock.Object);
    }

    [Fact]
    public async Task ValidConversion_ShouldReturnResponse()
    {
        var request = new ConversionRequest
        {
            Category = "Length",
            FromUnit = "meter",
            ToUnit = "foot",
            Value = 10
        };

        var response = await _service.ConvertAsync(request);

        Assert.Equal(10, response.OriginalValue);
        Assert.Equal(32.8084, response.ConvertedValue, 4);
        Assert.Equal("Length", response.Category);
    }

    [Fact]
    public async Task InvalidCategory_ShouldThrowException()
    {
        var request = new ConversionRequest
        {
            Category = "Invalid",
            FromUnit = "meter",
            ToUnit = "foot",
            Value = 10
        };

        await Assert.ThrowsAsync<UnsupportedConversionException>(() => _service.ConvertAsync(request));
    }

    [Fact]
    public async Task InvalidUnit_ShouldThrowException()
    {
        var request = new ConversionRequest
        {
            Category = "Length",
            FromUnit = "invalid",
            ToUnit = "foot",
            Value = 10
        };

        await Assert.ThrowsAsync<UnsupportedConversionException>(() => _service.ConvertAsync(request));
    }

    [Fact]
    public async Task IncompatibleConversion_ShouldThrowException()
    {
        var request = new ConversionRequest
        {
            Category = "Length",
            FromUnit = "meter",
            ToUnit = "celsius",
            Value = 10
        };

        await Assert.ThrowsAsync<UnsupportedConversionException>(() => _service.ConvertAsync(request));
    }
}

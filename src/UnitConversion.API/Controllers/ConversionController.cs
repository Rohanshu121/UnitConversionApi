using Microsoft.AspNetCore.Mvc;
using UnitConversion.Application.DTOs;
using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Exceptions;

namespace UnitConversion.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _conversionService;

    public ConversionController(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpPost("convert")]
    [ProducesResponseType(typeof(ConversionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Convert([FromBody] ConversionRequest request)
    {
        if (request == null)
            return BadRequest(new ErrorResponse { Error = "Request body cannot be empty." });

        if (string.IsNullOrWhiteSpace(request.Category))
            return BadRequest(new ErrorResponse { Error = "Category is required." });

        if (string.IsNullOrWhiteSpace(request.FromUnit))
            return BadRequest(new ErrorResponse { Error = "FromUnit is required." });

        if (string.IsNullOrWhiteSpace(request.ToUnit))
            return BadRequest(new ErrorResponse { Error = "ToUnit is required." });

        if (double.IsNaN(request.Value) || double.IsInfinity(request.Value))
            return BadRequest(new ErrorResponse { Error = "Value must be a valid number." });

        try
        {
            var result = await _conversionService.ConvertAsync(request);
            return Ok(result);
        }
        catch (UnsupportedConversionException ex)
        {
            return BadRequest(new ErrorResponse { Error = ex.Message });
        }
    }
}

public record ErrorResponse
{
    public string Error { get; init; } = string.Empty;
}

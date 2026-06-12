namespace UnitConversion.Domain.Exceptions;

public class UnsupportedConversionException : Exception
{
    public UnsupportedConversionException(string message) : base(message) { }
}

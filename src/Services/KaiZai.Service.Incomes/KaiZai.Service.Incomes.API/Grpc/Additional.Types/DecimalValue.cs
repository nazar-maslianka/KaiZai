namespace Grpc.Additional.Types;

/// <summary>
/// Represents a wrapper for a decimal value with additional conversion methods for gRPC.
/// </summary>
public sealed partial class DecimalValue
{
    private const decimal NanoFactor = 1_000_000_000;

    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalValue"/> class with the specified units and nanoseconds.
    /// </summary>
    /// <param name="units">The integral part of the decimal value.</param>
    /// <param name="nanos">The fractional part of the decimal value in nanoseconds.</param>
    public DecimalValue(long units, int nanos)
    {
        Units = units;
        Nanos = nanos;
    }

    /// <summary>
    /// Implicitly converts a <see cref="DecimalValue"/> to a <see cref="decimal"/>.
    /// </summary>
    /// <param name="grpcDecimal">The <see cref="DecimalValue"/> to convert.</param>
    /// <returns>The corresponding <see cref="decimal"/> value.</returns>
    public static implicit operator decimal(DecimalValue grpcDecimal)
        => grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;

    /// <summary>
    /// Implicitly converts a <see cref="decimal"/> to a <see cref="DecimalValue"/>.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> to convert.</param>
    /// <returns>A new instance of <see cref="DecimalValue"/> with the specified decimal value.</returns>
    public static implicit operator Grpc.Additional.Types.DecimalValue(decimal value)
    {
        var units = decimal.ToInt64(value);
        var nanos = decimal.ToInt32((value - units) * NanoFactor);
        return new Grpc.Additional.Types.DecimalValue(units, nanos);
    }
}


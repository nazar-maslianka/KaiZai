namespace Grpc.Additional.Types;

/// <summary>
/// Represents a wrapper for a GUID value with conversion methods.
/// </summary>
public sealed partial class GuidValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GuidValue"/> class with the specified GUID value.
    /// </summary>
    /// <param name="value">The string representation of the GUID value.</param>
    public GuidValue(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts the <see cref="GuidValue"/> to a <see cref="Guid"/>.
    /// </summary>
    /// <returns>The corresponding <see cref="Guid"/> value.</returns>
    private Guid ToGuid() 
        => Guid.Parse(Value);

    /// <summary>
    /// Converts a <see cref="Guid"/> to a <see cref="GuidValue"/>.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> to convert.</param>
    /// <returns>A new instance of <see cref="GuidValue"/> with the specified GUID value.</returns>
    private static GuidValue FromGuid(Guid guid)
        => new GuidValue(guid.ToString());

    /// <summary>
    /// Implicitly converts a <see cref="GuidValue"/> to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="customGuidValue">The <see cref="GuidValue"/> to convert.</param>
    /// <returns>The corresponding <see cref="Guid"/> value, or <see cref="Guid.Empty"/> if <paramref name="customGuidValue"/> is null.</returns>
    public static implicit operator Guid(GuidValue grpcGuidValue)
        => grpcGuidValue != null ? grpcGuidValue.ToGuid() : Guid.Empty;

    /// <summary>
    /// Implicitly converts a <see cref="Guid"/> to a <see cref="GuidValue"/>.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> to convert.</param>
    /// <returns>A new instance of <see cref="GuidValue"/> with the specified GUID value.</returns>
    public static implicit operator GuidValue(Guid guid)
        => FromGuid(guid);
}

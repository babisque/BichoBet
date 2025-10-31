using System.Text.Json.Serialization;

namespace BichoBet.Domain;

public record ErrorItem(
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("field")] string? Field,
    [property: JsonPropertyName("message")] string Message
);
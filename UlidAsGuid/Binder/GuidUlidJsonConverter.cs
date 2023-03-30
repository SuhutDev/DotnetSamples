using System.Text.Json.Serialization;
using System.Text.Json;

namespace UlidAsGuid.Binder;

public class GuidUlidJsonConverter : JsonConverter<Ulid>
{
    public override Ulid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Expected string");
            }

            Guid.TryParse(reader.GetString(), out var resultGuid);
            if (resultGuid.Equals(Guid.Empty))
            {
                throw new JsonException("Expected guid");
            }

            return new Ulid(resultGuid);
        }
        catch (IndexOutOfRangeException innerException)
        {
            throw new JsonException("Ulid invalid: length must be 26", innerException);
        }
        catch (OverflowException innerException2)
        {
            throw new JsonException("Ulid invalid: invalid character", innerException2);
        }
    }

    public override void Write(Utf8JsonWriter writer, Ulid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToGuid());
    }
}
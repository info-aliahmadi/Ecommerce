using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hydra.Infrastructure.Configuration
{
    /// <summary>
    /// Custom JSON converter for DateTime that supports multiple date formats
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string[] _dateFormats = new[]
        {
            "yyyy/MM/dd",
            "yyyy-MM-dd",
            "yyyy/MM/dd HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy/MM/ddTHH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy/MM/ddTHH:mm:ss.fffZ",
            "yyyy-MM-ddTHH:mm:ss.fffZ"
        };

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString();
                if (string.IsNullOrWhiteSpace(dateString))
                {
                    return default;
                }

                // Try parsing with multiple formats
                foreach (var format in _dateFormats)
                {
                    if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    {
                        // Convert to UTC
                        if (result.Kind == DateTimeKind.Unspecified)
                        {
                            result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
                        }
                        else
                        {
                            result = result.ToUniversalTime();
                        }
                        return result;
                    }
                }

                // Fallback to standard DateTime parsing
                if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fallbackResult))
                {
                    // Convert to UTC
                    if (fallbackResult.Kind == DateTimeKind.Unspecified)
                    {
                        fallbackResult = DateTime.SpecifyKind(fallbackResult, DateTimeKind.Utc);
                    }
                    else
                    {
                        fallbackResult = fallbackResult.ToUniversalTime();
                    }
                    return fallbackResult;
                }

                throw new JsonException($"Unable to convert \"{dateString}\" to DateTime.");
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return default;
            }

            throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
        }
    }

    /// <summary>
    /// Custom JSON converter for nullable DateTime that supports multiple date formats
    /// </summary>
    public class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        private readonly string[] _dateFormats = new[]
        {
            "yyyy/MM/dd",
            "yyyy-MM-dd",
            "yyyy/MM/dd HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy/MM/ddTHH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy/MM/ddTHH:mm:ss.fffZ",
            "yyyy-MM-ddTHH:mm:ss.fffZ"
        };

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString();
                if (string.IsNullOrWhiteSpace(dateString))
                {
                    return null;
                }

                // Try parsing with multiple formats
                foreach (var format in _dateFormats)
                {
                    if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    {
                        // Convert to UTC
                        if (result.Kind == DateTimeKind.Unspecified)
                        {
                            result = DateTime.SpecifyKind(result, DateTimeKind.Utc);
                        }
                        else
                        {
                            result = result.ToUniversalTime();
                        }
                        return result;
                    }
                }

                // Fallback to standard DateTime parsing
                if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fallbackResult))
                {
                    // Convert to UTC
                    if (fallbackResult.Kind == DateTimeKind.Unspecified)
                    {
                        fallbackResult = DateTime.SpecifyKind(fallbackResult, DateTimeKind.Utc);
                    }
                    else
                    {
                        fallbackResult = fallbackResult.ToUniversalTime();
                    }
                    return fallbackResult;
                }

                throw new JsonException($"Unable to convert \"{dateString}\" to DateTime?.");
            }

            throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}


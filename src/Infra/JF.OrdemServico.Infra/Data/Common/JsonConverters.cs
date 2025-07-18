using System.Text.Json;
using System.Text.Json.Serialization;

namespace JF.OrdemServico.Infra.Data.Common;

public class ComplexToStringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            if (doc.RootElement.TryGetProperty("Name", out var nameProp))
            {
                return nameProp.GetString();
            }
        }

        return reader.GetString(); // fallback caso seja string direto
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value); // para serializar simples string novamente
    }
}
using System.Text.Json.Serialization;

namespace Az.Functions.MetadataToJson.Models;

public class FunctionProperties
{
    [JsonPropertyName("IsCodeless")]
    public bool IsCodeless { get; set; }
}

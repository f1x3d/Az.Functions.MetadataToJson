using System.Text.Json.Serialization;

namespace Az.Functions.MetadataToJson.Models;

public class FunctionMetadata
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("scriptFile")]
    public string ScriptFile { get; set; } = string.Empty;

    [JsonPropertyName("entryPoint")]
    public string EntryPoint { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public FunctionProperties Properties { get; set; } = new();

    [JsonPropertyName("bindings")]
    public IEnumerable<FunctionBinding> Bindings { get; set; } = new List<FunctionBinding>();
}

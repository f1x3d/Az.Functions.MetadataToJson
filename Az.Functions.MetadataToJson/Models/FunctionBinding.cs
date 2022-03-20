using System.Text.Json.Serialization;

namespace Az.Functions.MetadataToJson.Models;

public class FunctionBinding
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("direction")]
    public string Direction { get; set; } = string.Empty;

    [JsonPropertyName("queueName")]
    public string QueueName { get; set; } = string.Empty;

    [JsonPropertyName("connection")]
    public string Connection { get; set; } = string.Empty;
}

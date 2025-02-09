using System.Text.Json.Serialization;

namespace Oid85.HomeBot.External.ResourceStore.Models;

public class Range<T>
{
    [JsonPropertyName("max")]
    public T Max { get; set; }
    
    [JsonPropertyName("min")]
    public T Min { get; set; }
}
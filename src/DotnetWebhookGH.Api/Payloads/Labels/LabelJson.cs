namespace DotnetWebhookGH.Api.Payloads.Labels;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class LabelJson
{
    [Required, Range(1, long.MaxValue)]
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [Required, StringLength(50)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [Required, StringLength(6)]
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [Required, StringLength(100)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Required]
    [JsonPropertyName("default")]
    public bool? IsDefault { get; set; }
}

namespace DotnetWebhookGH.Api.Payloads.Reactions;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ReactionJson
{
    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("total_count")]
    public int? TotalCount { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("+1")]
    public int? OnePlus { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("-1")]
    public int? OneMinus { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("laugh")]
    public int? Laugh { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("hooray")]
    public int? Hooray { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("confused")]
    public int? Confused { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("heart")]
    public int? Heart { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("rocket")]
    public int? Rocket { get; set; }

    [Required, Range(0, int.MaxValue)]
    [JsonPropertyName("eyes")]
    public int? Eyes { get; set; }
}

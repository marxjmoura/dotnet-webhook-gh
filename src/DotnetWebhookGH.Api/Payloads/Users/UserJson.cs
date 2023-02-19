namespace DotnetWebhookGH.Api.Payloads.Users;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UserJson
{
    [Required, Range(1, long.MaxValue)]
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [Required, StringLength(50)]
    [JsonPropertyName("login")]
    public string? Login { get; set; }
}

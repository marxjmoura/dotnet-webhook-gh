namespace DotnetWebhookGH.Api.Payloads.Repositories;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DotnetWebhookGH.Api.Payloads.Users;

public class RepositoryJson
{
    [Required, Range(1, long.MaxValue)]
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [Required, StringLength(100)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [Required]
    [JsonPropertyName("default_branch")]
    public string? DefaultBranch { get; set; }

    [Required]
    [JsonPropertyName("private")]
    public bool? IsPrivate { get; set; }

    [Required]
    [JsonPropertyName("fork")]
    public bool? IsFork { get; set; }

    [Required]
    [JsonPropertyName("topics")]
    public IEnumerable<string>? Topics { get; set; }

    [Required]
    [JsonPropertyName("owner")]
    public UserJson? Owner { get; set; }
}

namespace DotnetWebhookGH.Api.Payloads.Issues;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Payloads.Labels;
using DotnetWebhookGH.Api.Payloads.Reactions;
using DotnetWebhookGH.Api.Payloads.Users;

public class IssueJson
{
    [Required, Range(1, int.MaxValue)]
    [JsonPropertyName("number")]
    public int? Number { get; set; }

    [Required, StringLength(1024)]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [Required]
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [Required]
    [JsonPropertyName("state")]
    public IssueState? State { get; set; }

    [Required]
    [JsonPropertyName("locked")]
    public bool? Locked { get; set; }

    [Required]
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Required]
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("closed_at")]
    public DateTime? ClosedAt { get; set; }

    [JsonPropertyName("reactions")]
    public ReactionJson? Reactions { get; set; }

    [Required]
    [JsonPropertyName("assignees")]
    public IEnumerable<UserJson>? Assignees { get; set; }

    [Required]
    [JsonPropertyName("labels")]
    public IEnumerable<LabelJson>? Labels { get; set; }
}

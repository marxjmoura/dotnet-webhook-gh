namespace DotnetWebhookGH.Api.Payloads.Events;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Payloads.Issues;
using DotnetWebhookGH.Api.Payloads.Repositories;
using DotnetWebhookGH.Api.Payloads.Users;

public class EventJson
{
    [Required]
    [JsonPropertyName("action")]
    public IssueEvent? Action { get; set; }

    [Required]
    [JsonPropertyName("issue")]
    public IssueJson? Issue { get; set; }

    [Required]
    [JsonPropertyName("repository")]
    public RepositoryJson? Repository { get; set; }

    [Required]
    [JsonPropertyName("sender")]
    public UserJson? Sender { get; set; }
}

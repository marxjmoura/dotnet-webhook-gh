namespace DotnetWebhookGH.Api.Data.Model.Issues;

using DotnetWebhookGH.Api.Data.Model.Labels;
using DotnetWebhookGH.Api.Data.Model.Reactions;
using DotnetWebhookGH.Api.Data.Model.Repositories;
using DotnetWebhookGH.Api.Data.Model.Users;

public class Issue
{
    public string? Delivery { get; set; }

    public IssueEvent? Event { get; set; }

    public int Number { get; set; }

    public long RepositoryId { get; set; }

    public long SenderId { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public IssueState State { get; set; }

    public bool Locked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public Reaction? Reactions { get; set; }

    public Repository? Repository { get; set; }

    public User? Sender { get; set; }

    public ICollection<User>? Assignees { get; set; } = new HashSet<User>();

    public ICollection<Label>? Labels { get; set; } = new HashSet<Label>();
}

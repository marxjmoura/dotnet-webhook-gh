using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Repositories;

namespace DotnetWebhookGH.Api.Data.Model.Users;

public class User
{
    public long Id { get; set; }

    public string? Login { get; set; }

    public ICollection<Repository>? Repositories { get; set; } = new HashSet<Repository>();

    public ICollection<Issue>? AssignedIssues { get; set; } = new HashSet<Issue>();

    public ICollection<Issue>? SentIssues { get; set; } = new HashSet<Issue>();
}

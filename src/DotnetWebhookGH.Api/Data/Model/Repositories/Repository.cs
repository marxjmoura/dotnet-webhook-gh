namespace DotnetWebhookGH.Api.Data.Model.Repositories;

using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Users;

public class Repository
{
    public long Id { get; set; }

    public long OwnerId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Language { get; set; }

    public string? DefaultBranch { get; set; }

    public string? Topics { get; set; }

    public bool IsPrivate { get; set; }

    public bool IsFork { get; set; }

    public User? Owner { get; set; }

    public ICollection<Issue>? Issues { get; set; } = new HashSet<Issue>();
}

namespace DotnetWebhookGH.Api.Data.Model.Labels;

using DotnetWebhookGH.Api.Data.Model.Issues;

public class Label
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Color { get; set; }

    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public ICollection<Issue>? Issues { get; set; } = new HashSet<Issue>();
}

namespace DotnetWebhookGH.Api.Payloads;

public class EventRouteParams
{
    /// <summary>
    /// Repository owner (user or organization).
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// Repository name.
    /// </summary>
    public string? Repo { get; set; }

    /// <summary>
    /// Issue number (same as used on GitHub).
    /// </summary>
    public int Number { get; set; }
}

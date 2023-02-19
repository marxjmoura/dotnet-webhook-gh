namespace DotnetWebhookGH.Api.Data.Model.Reactions;

using DotnetWebhookGH.Api.Data.Model.Issues;

public class Reaction
{
    public string? Delivery { get; set; }

    public int TotalCount { get; set; }

    public int OnePlus { get; set; }

    public int OneMinus { get; set; }

    public int Laugh { get; set; }

    public int Hooray { get; set; }

    public int Confused { get; set; }

    public int Heart { get; set; }

    public int Rocket { get; set; }

    public int Eyes { get; set; }

    public Issue? Issue { get; set; }
}

namespace DotnetWebhookGH.Api.Payloads.Issues;

public static class IssueJsonMap
{
    public static IEnumerable<long> AllLabelIds(this IssueJson payload)
    {
        return payload.Labels!.Select(label => label.Id!.Value);
    }
}

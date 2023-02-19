namespace DotnetWebhookGH.Api.Data.Model.Labels;

public static class LabelsDBQuery
{
    public static IQueryable<Label> WhereIdIn(this IQueryable<Label> labels, IEnumerable<long> ids)
    {
        return labels.Where(label => ids.Contains(label.Id));
    }
}

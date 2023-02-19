namespace DotnetWebhookGH.Api.Payloads.Repositories;

using DotnetWebhookGH.Api.Data.Model.Labels;
using DotnetWebhookGH.Api.Payloads.Labels;

public static class LabelJsonMap
{
    public static IEnumerable<Label> Map(this IEnumerable<LabelJson> payloads, IEnumerable<Label> labels)
    {
        return payloads.Select(payload => payload.Map(labels));
    }

    public static Label Map(this LabelJson payload, IEnumerable<Label> labels)
    {
        var label = labels.SingleOrDefault(label => label.Id == payload.Id);

        label ??= new Label { Id = payload.Id!.Value };
        label.Name = payload.Name;
        label.Color = payload.Color;
        label.Description = payload.Description;
        label.IsDefault = payload.IsDefault!.Value;

        return label;
    }

    public static LabelJson ToJson(this Label label)
    {
        var payload = new LabelJson();
        payload.Id = label.Id;
        payload.Name = label.Name;
        payload.Color = label.Color;
        payload.Description = label.Description;
        payload.IsDefault = label.IsDefault;

        return payload;
    }
}

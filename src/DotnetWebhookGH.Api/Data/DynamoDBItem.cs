namespace DotnetWebhookGH.Api.Data.Model;

using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DotnetWebhookGH.Api.Data.DynamoDB;
using System.Text.Json.Nodes;

public class DynamoDBItem
{
    private readonly JsonNode _json;

    public DynamoDBItem(JsonNode json)
    {
        _json = json;
    }

    public Dictionary<string, AttributeValue> ToAttributeMap(string @event)
    {
        var repository = _json["repository"]!["full_name"]!.GetValue<string>();
        var number = _json["issue"]!["number"]!.GetValue<int>();
        var updatedAt = _json["issue"]!["updated_at"]!.GetValue<string>();

        var attributes = JsonNode.Parse("{}")!;
        attributes[DynamoDBTable.PK] = $"{repository}/{@event}";
        attributes[DynamoDBTable.SK] = $"#{number} {updatedAt}";
        attributes["payload"] = _json;

        return Document.FromJson(attributes.ToJsonString()).ToAttributeMap();
    }
}

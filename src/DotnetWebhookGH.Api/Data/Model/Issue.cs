namespace DotnetWebhookGH.Api.Data.Model;

using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DotnetWebhookGH.Api.Data.DynamoDB;
using System.Text.Json.Nodes;

public class Issue : IModel
{
    public Dictionary<string, AttributeValue> ToDynamoDBItem(JsonNode json)
    {
        var repository = json["repository"]!["full_name"]!.GetValue<string>();
        var number = json["issue"]!["number"]!.GetValue<int>();
        var updatedAt = json["issue"]!["updated_at"]!.GetValue<string>();

        var attributes = JsonNode.Parse(json["issue"]!.ToJsonString())!;
        attributes[DynamoDBTable.PK] = $"{repository}/issues";
        attributes[DynamoDBTable.SK] = $"#{number} {updatedAt}";

        return Document.FromJson(attributes.ToJsonString()).ToAttributeMap();
    }
}

namespace DotnetWebhookGH.Api.Data.Model;

using System.Text.Json.Nodes;
using Amazon.DynamoDBv2.Model;

public interface IModel
{
    Dictionary<string, AttributeValue> ToDynamoDBItem(JsonNode json);
}

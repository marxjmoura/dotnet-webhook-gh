using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DotnetWebhookGH.Api.Data.DynamoDB;

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

    public Dictionary<string, Condition> ToKeyConditions() => new()
    {
        [DynamoDBTable.PK] = new()
        {
            ComparisonOperator = ComparisonOperator.EQ,
            AttributeValueList = { new($"{Owner}/{Repo}/issues") }
        },
        [DynamoDBTable.SK] = new()
        {
            ComparisonOperator = ComparisonOperator.BEGINS_WITH,
            AttributeValueList = { new($"#{Number} ") }
        }
    };
}

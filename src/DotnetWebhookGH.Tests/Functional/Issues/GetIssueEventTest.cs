namespace DotnetWebhookGH.Tests.Functional.Issues;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xunit;

public class GetIssueEventTest
{
    [Fact]
    public async Task ShouldReturn200WithEventArray()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        var fixture = Path.Combine(AppContext.BaseDirectory, "Fixtures/issue.json");
        var fixtureJson = await File.ReadAllTextAsync(fixture);

        var dynamoDBItemJson = JsonSerializer.Serialize(new
        {
            PK = "marxjmoura/dotnet-webhook-gh/issues",
            SK = "#1 2023-02-11T16:22:42Z",
            payload = JsonSerializer.Deserialize<dynamic>(fixtureJson)
        });

        server.Services.GetService<IAmazonDynamoDB>()!
            .QueryAsync(Arg.Any<QueryRequest>())
            .Returns(new QueryResponse
            {
                Items = { Document.FromJson(dynamoDBItemJson).ToAttributeMap() }
            });

        var response = await client.GetAsync("/marxjmoura/dotnet-webhook-gh/issues/1/events");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJsonArray = JsonSerializer.Deserialize<JsonNode[]>(responseContent)!;
        var responseJson = responseJsonArray.SingleOrDefault();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(1, responseJsonArray.Length);
        Assert.Equal("opened", responseJson?["action"]?.GetValue<string>());
        Assert.Equal(1, responseJson?["issue"]?["number"]?.GetValue<int>());
        Assert.Equal("The challenge has begun!", responseJson?["issue"]?["title"]?.GetValue<string>());
    }
}

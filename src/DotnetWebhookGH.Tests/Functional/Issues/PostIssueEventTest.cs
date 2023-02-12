namespace DotnetWebhookGH.Tests.Functional.Issues;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DotnetWebhookGH.Api.Data.DynamoDB;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class PostIssueEventTest
{
    [Fact]
    public async Task ShouldSaveToDatabaseAndReturn204()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        client.DefaultRequestHeaders.Add("X-GitHub-Event", "issues");

        var dynamoDB = server.Services.GetService<IAmazonDynamoDB>()!;
        var savedItem = null as Dictionary<string, AttributeValue>;

        await dynamoDB.PutItemAsync(DynamoDBTable.Name,
            Arg.Do<Dictionary<string, AttributeValue>>(item => savedItem = item));

        var fixture = Path.Combine(AppContext.BaseDirectory, "Fixtures/Issue.json");
        var requestJson = await File.ReadAllTextAsync(fixture);
        var request = new StringContent(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("/webhook", request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal("marxjmoura/dotnet-webhook-gh/issues", savedItem?[DynamoDBTable.PK].S);
        Assert.Equal("#1 2023-02-11T16:22:42Z", savedItem?[DynamoDBTable.SK].S);
        Assert.Equal("1580911028", savedItem?["payload"].M["issue"].M["id"].N);
    }
}

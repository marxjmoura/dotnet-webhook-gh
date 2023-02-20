namespace DotnetWebhookGH.Tests.Functional.Issues;

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DotnetWebhookGH.Api.Data;
using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Users;
using DotnetWebhookGH.Api.Payloads.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class PostIssueEventAlreadyReceivedTest
{
    [Fact]
    public async Task ShouldIgnoreAndReturn204()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        client.DefaultRequestHeaders.Add("X-GitHub-Event", "issues");
        client.DefaultRequestHeaders.Add("X-GitHub-Delivery", "87ecca00-aa63-11ed-9647-af96ab45e150");

        var fixture = Path.Combine(AppContext.BaseDirectory, $"Fixtures/Issue.Opened.json");
        var fixtureJson = await File.ReadAllTextAsync(fixture);

        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());

        var existentIssue = new Issue { Delivery = "87ecca00-aa63-11ed-9647-af96ab45e150" };
        var payload = JsonSerializer.Deserialize<EventJson>(fixtureJson, options)!;
        payload.Map(existentIssue, knownUsers: Enumerable.Empty<User>());

        var dbContext = server.Services.GetService<ApiDbContext>()!;
        dbContext.Issues.Add(existentIssue);
        dbContext.SaveChanges();

        var request = new StringContent(fixtureJson, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("/webhook/issues", request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

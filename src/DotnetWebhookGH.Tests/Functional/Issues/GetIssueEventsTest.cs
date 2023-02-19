namespace DotnetWebhookGH.Tests.Functional.Issues;

using DotnetWebhookGH.Api.Data;
using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Users;
using DotnetWebhookGH.Api.Payloads.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

public class GetIssueEventsTest
{
    [Theory]
    [InlineData(IssueEvent.Assigned, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Closed, IssueState.Closed, "2023-02-20T08:21:38Z", false)]
    [InlineData(IssueEvent.Deleted, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Demilestoned, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Edited, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Labeled, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Locked, IssueState.Open, null, true)]
    [InlineData(IssueEvent.Milestoned, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Opened, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Pinned, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Reopened, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Transferred, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Unassigned, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Unlabeled, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Unlocked, IssueState.Open, null, false)]
    [InlineData(IssueEvent.Unpinned, IssueState.Open, null, false)]
    public async Task ShouldReturn200WithEventArray(IssueEvent @event,
        IssueState state, string closedAt, bool locked)
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        var fixture = Path.Combine(AppContext.BaseDirectory, $"Fixtures/Issue.{@event}.json");
        var fixtureJson = await File.ReadAllTextAsync(fixture);

        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());

        var issue = new Issue { Delivery = Guid.NewGuid().ToString() };
        var payload = JsonSerializer.Deserialize<EventJson>(fixtureJson, options)!;
        payload.Map(issue, Enumerable.Empty<User>());

        var dbContext = server.Services.GetService<ApiDbContext>()!;
        dbContext.Issues.Add(issue);
        dbContext.SaveChanges();

        var response = await client.GetAsync("/marxjmoura/dotnet-webhook-gh/issues/1/events");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJsonArray = JsonSerializer.Deserialize<EventJson[]>(responseContent, options)!;
        var responseJson = responseJsonArray.SingleOrDefault();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(@event, responseJson?.Action);
        Assert.Single(responseJsonArray);

        Assert.NotNull(responseJson?.Issue);
        Assert.Equal(1, responseJson?.Issue.Number);
        Assert.Equal("The challenge has begun!", responseJson?.Issue.Title);
        Assert.Equal("Catch this!", responseJson?.Issue.Body);
        Assert.Equal(state, responseJson?.Issue.State);
        Assert.Equal(locked, responseJson?.Issue.Locked);
        Assert.Equal("2023-02-11T16:22:42Z", responseJson?.Issue.CreatedAt!.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal("2023-02-11T16:32:43Z", responseJson?.Issue.UpdatedAt!.Value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal(closedAt, responseJson?.Issue.ClosedAt?.ToString("yyyy-MM-ddTHH:mm:ssZ"));

        Assert.NotNull(responseJson?.Issue.Reactions);
        Assert.Equal(8, responseJson?.Issue.Reactions.TotalCount);
        Assert.Equal(1, responseJson?.Issue.Reactions.OnePlus);
        Assert.Equal(1, responseJson?.Issue.Reactions.OneMinus);
        Assert.Equal(1, responseJson?.Issue.Reactions.Laugh);
        Assert.Equal(1, responseJson?.Issue.Reactions.Hooray);
        Assert.Equal(1, responseJson?.Issue.Reactions.Confused);
        Assert.Equal(1, responseJson?.Issue.Reactions.Heart);
        Assert.Equal(1, responseJson?.Issue.Reactions.Rocket);
        Assert.Equal(1, responseJson?.Issue.Reactions.Eyes);

        Assert.NotNull(responseJson?.Issue.Assignees);
        Assert.All(responseJson?.Issue.Assignees!, assignee =>
        {
            Assert.Equal(9284822, assignee.Id);
            Assert.Equal("marxjmoura", assignee.Login);
        });

        Assert.NotNull(responseJson?.Issue.Labels);
        Assert.All(responseJson?.Issue.Labels!, label =>
        {
            Assert.Equal(5142952025, label.Id);
            Assert.Equal("good first issue", label.Name);
            Assert.Equal("Good for newcomers", label.Description);
            Assert.Equal("7057ff", label.Color);
            Assert.True(label.IsDefault);
        });

        Assert.NotNull(responseJson?.Repository);
        Assert.Equal(600420038, responseJson?.Repository.Id);
        Assert.Equal("dotnet-webhook-gh", responseJson?.Repository.Name);
        Assert.Equal(".NET Core webhook to listen for GitHub events.", responseJson?.Repository.Description);
        Assert.Equal("C#", responseJson?.Repository.Language);
        Assert.Equal("main", responseJson?.Repository.DefaultBranch);
        Assert.Equal(new[] { "challenge", "aws", "netcore" }, responseJson?.Repository.Topics!);
        Assert.False(responseJson?.Repository.IsPrivate);
        Assert.False(responseJson?.Repository.IsFork);

        Assert.NotNull(responseJson?.Repository!.Owner);
        Assert.Equal(9284822, responseJson?.Repository!.Owner.Id);
        Assert.Equal("marxjmoura", responseJson?.Repository!.Owner.Login);

        Assert.NotNull(responseJson?.Sender);
        Assert.Equal(9284822, responseJson?.Sender.Id);
        Assert.Equal("marxjmoura", responseJson?.Sender.Login);
    }

    [Fact]
    public async Task ShouldReturn200WithEmptyArrayWhenNotFound()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        var response = await client.GetAsync("/marxjmoura/dotnet-webhook-gh/issues/1/events");
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJsonArray = JsonSerializer.Deserialize<EventJson[]>(responseContent)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Empty(responseJsonArray);
    }
}

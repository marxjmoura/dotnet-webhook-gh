namespace DotnetWebhookGH.Tests.Functional.Issues;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DotnetWebhookGH.Api.Data;
using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class PostIssueEventTest
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
    public async Task ShouldSaveToDatabaseAndReturn204(IssueEvent @event,
        IssueState state, string closedAt, bool locked)
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        client.DefaultRequestHeaders.Add("X-GitHub-Event", "issues");
        client.DefaultRequestHeaders.Add("X-GitHub-Delivery", "87ecca00-aa63-11ed-9647-af96ab45e150");

        var fixture = Path.Combine(AppContext.BaseDirectory, $"Fixtures/Issue.{@event}.json");
        var requestJson = await File.ReadAllTextAsync(fixture);
        var request = new StringContent(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("/webhook/issues", request);

        var dbContext = server.Services.GetService<ApiDbContext>()!;

        var savedIssue = await dbContext.Issues
            .IncludeRepository()
            .IncludeSender()
            .IncludeAssignees()
            .IncludeReactions()
            .IncludeLabels()
            .SingleOrDefaultAsync();

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        Assert.NotNull(savedIssue);
        Assert.Equal("87ecca00-aa63-11ed-9647-af96ab45e150", savedIssue.Delivery);
        Assert.Equal(@event, savedIssue.Event);
        Assert.Equal(1, savedIssue.Number);
        Assert.Equal("The challenge has begun!", savedIssue.Title);
        Assert.Equal("Catch this!", savedIssue.Body);
        Assert.Equal(state, savedIssue.State);
        Assert.Equal(locked, savedIssue.Locked);
        Assert.Equal("2023-02-11T16:22:42Z", savedIssue.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal("2023-02-11T16:32:43Z", savedIssue.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        Assert.Equal(closedAt, savedIssue.ClosedAt?.ToString("yyyy-MM-ddTHH:mm:ssZ"));

        Assert.NotNull(savedIssue.Reactions);
        Assert.Equal(8, savedIssue.Reactions.TotalCount);
        Assert.Equal(1, savedIssue.Reactions.OnePlus);
        Assert.Equal(1, savedIssue.Reactions.OneMinus);
        Assert.Equal(1, savedIssue.Reactions.Laugh);
        Assert.Equal(1, savedIssue.Reactions.Hooray);
        Assert.Equal(1, savedIssue.Reactions.Confused);
        Assert.Equal(1, savedIssue.Reactions.Heart);
        Assert.Equal(1, savedIssue.Reactions.Rocket);
        Assert.Equal(1, savedIssue.Reactions.Eyes);

        Assert.NotNull(savedIssue.Assignees);
        Assert.All(savedIssue.Assignees!, assignee =>
        {
            Assert.Equal(9284822, assignee.Id);
            Assert.Equal("marxjmoura", assignee.Login);
        });

        Assert.NotNull(savedIssue.Labels);
        Assert.All(savedIssue.Labels!, label =>
        {
            Assert.Equal(5142952025, label.Id);
            Assert.Equal("good first issue", label.Name);
            Assert.Equal("Good for newcomers", label.Description);
            Assert.Equal("7057ff", label.Color);
            Assert.True(label.IsDefault);
        });

        Assert.NotNull(savedIssue.Repository);
        Assert.Equal(600420038, savedIssue.Repository.Id);
        Assert.Equal("dotnet-webhook-gh", savedIssue.Repository.Name);
        Assert.Equal(".NET Core webhook to listen for GitHub events.", savedIssue.Repository.Description);
        Assert.Equal("C#", savedIssue.Repository.Language);
        Assert.Equal("main", savedIssue.Repository.DefaultBranch);
        Assert.Equal("challenge, aws, netcore", savedIssue.Repository.Topics);
        Assert.False(savedIssue.Repository.IsPrivate);
        Assert.False(savedIssue.Repository.IsFork);

        Assert.NotNull(savedIssue.Repository!.Owner);
        Assert.Equal(9284822, savedIssue.Repository!.Owner.Id);
        Assert.Equal("marxjmoura", savedIssue.Repository!.Owner.Login);

        Assert.NotNull(savedIssue.Sender);
        Assert.Equal(9284822, savedIssue.Sender.Id);
        Assert.Equal("marxjmoura", savedIssue.Sender.Login);
    }

    [Fact]
    public async Task ShouldReturn400()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        client.DefaultRequestHeaders.Add("X-GitHub-Event", "issues");
        client.DefaultRequestHeaders.Add("X-GitHub-Delivery", "87ecca00-aa63-11ed-9647-af96ab45e150");

        var request = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("/webhook/issues", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

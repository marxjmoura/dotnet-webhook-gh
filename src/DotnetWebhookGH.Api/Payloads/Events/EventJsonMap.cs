namespace DotnetWebhookGH.Api.Payloads.Events;

using DotnetWebhookGH.Api.Data.Model.Issues;
using DotnetWebhookGH.Api.Data.Model.Users;
using DotnetWebhookGH.Api.Payloads.Issues;
using DotnetWebhookGH.Api.Payloads.Repositories;
using DotnetWebhookGH.Api.Payloads.Users;

public static class EventJsonMap
{
    public static void Map(this EventJson payload, Issue issue, IEnumerable<User> knownUsers)
    {
        var users = payload.ReferencedUsers(knownUsers);

        issue.Event = payload.Action;
        issue.Number = payload.Issue!.Number!.Value;
        issue.Title = payload.Issue.Title;
        issue.Body = payload.Issue.Body;
        issue.State = payload.Issue.State!.Value;
        issue.Locked = payload.Issue.Locked!.Value;
        issue.CreatedAt = payload.Issue.CreatedAt!.Value;
        issue.UpdatedAt = payload.Issue.UpdatedAt!.Value;
        issue.ClosedAt = payload.Issue.ClosedAt;
        issue.Reactions = payload.Issue.Reactions!.Map(issue.Delivery!);
        issue.Sender = payload.Sender!.Map(users);
        issue.Repository = payload.Repository!.Map(issue.Repository, users);
        issue.Assignees = payload.Issue.Assignees!.Map(users).ToList();
        issue.Labels = payload.Issue.Labels!.Map(issue.Labels!).ToList();
    }

    public static IEnumerable<User> ReferencedUsers(this EventJson payload, IEnumerable<User> knownUsers)
    {
        var knownUserIds = knownUsers.Select(user => user.Id);

        var newUsers = payload.Issue!.Assignees!
            .Select(nestedPayload => nestedPayload.Map(knownUsers))
            .Concat(new[]
            {
                payload.Sender!.Map(knownUsers),
                payload.Repository!.Owner!.Map(knownUsers)
            })
            .Where(user => !knownUserIds.Contains(user.Id))
            .DistinctBy(user => user.Id);

        return knownUsers.Concat(newUsers).ToList();
    }

    public static IEnumerable<long> ReferencedUserIds(this EventJson payload)
    {
        return payload.Issue!.Assignees!
            .Select(nestedPayload => nestedPayload.Id!.Value)
            .Concat(new[]
            {
                payload.Sender!.Id!.Value,
                payload.Repository!.Owner!.Id!.Value
            })
            .Distinct();
    }

    public static EventJson ToJson(this Issue issue)
    {
        var payload = new EventJson();
        payload.Action = issue.Event;

        payload.Issue = new IssueJson();
        payload.Issue.Number = issue.Number;
        payload.Issue.Title = issue.Title;
        payload.Issue.Body = issue.Body;
        payload.Issue.State = issue.State;
        payload.Issue.Locked = issue.Locked;
        payload.Issue.CreatedAt = issue.CreatedAt;
        payload.Issue.UpdatedAt = issue.UpdatedAt;
        payload.Issue.ClosedAt = issue.ClosedAt;

        payload.Issue.Reactions = issue.Reactions!.ToJson();

        payload.Issue.Assignees = issue.Assignees!
            .Select(assignee => assignee.ToJson())
            .ToList();

        payload.Issue.Labels = issue.Labels!
            .Select(label => label.ToJson())
            .ToList();

        payload.Repository = issue.Repository!.ToJson();

        payload.Sender = issue.Sender!.ToJson();

        return payload;
    }
}

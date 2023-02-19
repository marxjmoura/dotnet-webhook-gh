using DotnetWebhookGH.Api.Data.Model.Issues;
using Microsoft.EntityFrameworkCore;

namespace DotnetWebhookGH.Api.Data.Model.Users;

public static class IssueDBQuery
{
    public static IQueryable<Issue> IncludeAssignees(this IQueryable<Issue> issues)
    {
        return issues.Include(issue => issue.Assignees);
    }

    public static IQueryable<Issue> IncludeLabels(this IQueryable<Issue> issues)
    {
        return issues.Include(issue => issue.Labels);
    }

    public static IQueryable<Issue> IncludeReactions(this IQueryable<Issue> issues)
    {
        return issues.Include(issue => issue.Reactions);
    }

    public static IQueryable<Issue> IncludeRepository(this IQueryable<Issue> issues)
    {
        return issues.Include(issue => issue.Repository).ThenInclude(repository => repository!.Owner);
    }

    public static IQueryable<Issue> IncludeSender(this IQueryable<Issue> issues)
    {
        return issues.Include(issue => issue.Sender);
    }

    public static IQueryable<Issue> WhereDelivery(this IQueryable<Issue> issues, string delivery)
    {
        return issues.Where(issue => issue.Delivery == delivery);
    }

    public static IQueryable<Issue> WhereNumber(this IQueryable<Issue> issues, int number)
    {
        return issues.Where(issue => issue.Number == number);
    }

    public static IQueryable<Issue> WhereRepositoryNameEqual(this IQueryable<Issue> issues, string name)
    {
        name = name.ToLower();
        return issues.Where(issue => issue.Repository!.Name == name);
    }

    public static IQueryable<Issue> WhereRepositoryOwnerEqual(this IQueryable<Issue> issues, string login)
    {
        login = login.ToLower();
        return issues.Where(issue => issue.Repository!.Owner!.Login == login);
    }
}

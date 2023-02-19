namespace DotnetWebhookGH.Api.Data.Model.Users;

public static class UserDBQuery
{
    public static IQueryable<User> WhereIdIn(this IQueryable<User> users, IEnumerable<long> ids)
    {
        return users.Where(user => ids.Contains(user.Id));
    }
}

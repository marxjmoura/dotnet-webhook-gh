namespace DotnetWebhookGH.Api.Data.Model.Repositories;

public static class RepositoryDBQuery
{
    public static IQueryable<Repository> WhereId(this IQueryable<Repository> repositories, long id)
    {
        return repositories.Where(user => user.Id == id);
    }
}

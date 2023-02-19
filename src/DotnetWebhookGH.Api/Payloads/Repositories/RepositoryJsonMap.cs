namespace DotnetWebhookGH.Api.Payloads.Repositories;

using DotnetWebhookGH.Api.Data.Model.Repositories;
using DotnetWebhookGH.Api.Data.Model.Users;
using DotnetWebhookGH.Api.Payloads.Users;

public static class RepositoryJsonMap
{
    static readonly string TopicSeparator = ", ";

    public static Repository Map(this RepositoryJson payload, Repository? repository, IEnumerable<User> users)
    {
        repository ??= new Repository { Id = payload.Id!.Value };
        repository.Name = payload.Name!.ToLower();
        repository.Description = payload.Description;
        repository.Language = payload.Language;
        repository.DefaultBranch = payload.DefaultBranch;
        repository.IsPrivate = payload.IsPrivate!.Value;
        repository.IsFork = payload.IsFork!.Value;
        repository.Owner = payload.Owner!.Map(users);

        if (payload.Topics != null)
        {
            repository.Topics = string.Join(TopicSeparator, payload.Topics);
        }

        return repository;
    }

    public static RepositoryJson ToJson(this Repository repository)
    {
        var payload = new RepositoryJson();
        payload.Id = repository.Id;
        payload.Name = repository.Name;
        payload.Description = repository.Description;
        payload.Language = repository.Language;
        payload.DefaultBranch = repository.DefaultBranch;
        payload.IsPrivate = repository.IsPrivate;
        payload.IsFork = repository.IsFork;

        payload.Owner = repository.Owner!.ToJson();

        payload.Topics = repository.Topics?
            .Split(TopicSeparator)
            .Where(topic => !string.IsNullOrEmpty(topic))
            .ToList();

        return payload;
    }
}

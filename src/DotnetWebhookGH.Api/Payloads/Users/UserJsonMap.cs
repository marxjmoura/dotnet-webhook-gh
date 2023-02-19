using DotnetWebhookGH.Api.Data.Model.Users;

namespace DotnetWebhookGH.Api.Payloads.Users;

public static class UserJsonMap
{
    public static IEnumerable<User> Map(this IEnumerable<UserJson> payloads, IEnumerable<User> users)
    {
        return payloads.Select(payload => payload.Map(users)).ToList();
    }

    public static User Map(this UserJson payload, IEnumerable<User> users)
    {
        var user = users.SingleOrDefault(user => user.Id == payload.Id);

        user ??= new User { Id = payload.Id!.Value };
        user.Login = payload.Login!.ToLower();

        return user;
    }

    public static UserJson ToJson(this User user)
    {
        var payload = new UserJson();
        payload.Id = user.Id;
        payload.Login = user.Login;

        return payload;
    }
}

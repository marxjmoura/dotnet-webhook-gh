namespace DotnetWebhookGH.Api.Payloads.Repositories;

using DotnetWebhookGH.Api.Data.Model.Reactions;
using DotnetWebhookGH.Api.Payloads.Reactions;

public static class ReactionJsonMap
{
    public static Reaction Map(this ReactionJson payload, string delivery)
    {
        var reaction = new Reaction();
        reaction.Delivery = delivery;
        reaction.TotalCount = payload.TotalCount!.Value;
        reaction.OnePlus = payload.OnePlus!.Value;
        reaction.OneMinus = payload.OneMinus!.Value;
        reaction.Laugh = payload.Laugh!.Value;
        reaction.Hooray = payload.Hooray!.Value;
        reaction.Confused = payload.Confused!.Value;
        reaction.Heart = payload.Heart!.Value;
        reaction.Rocket = payload.Rocket!.Value;
        reaction.Eyes = payload.Eyes!.Value;

        return reaction;
    }

    public static ReactionJson ToJson(this Reaction reaction)
    {
        var payload = new ReactionJson();
        payload.TotalCount = reaction.TotalCount;
        payload.OnePlus = reaction.OnePlus;
        payload.OneMinus = reaction.OneMinus;
        payload.Laugh = reaction.Laugh;
        payload.Hooray = reaction.Hooray;
        payload.Confused = reaction.Confused;
        payload.Heart = reaction.Heart;
        payload.Rocket = reaction.Rocket;
        payload.Eyes = reaction.Eyes;

        return payload;
    }
}

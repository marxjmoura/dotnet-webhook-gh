namespace DotnetWebhookGH.Api.Configuration;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

public static class JsonConfiguration
{
    public static void AddApiJsonSerializerOptions(this JsonOptions options)
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}

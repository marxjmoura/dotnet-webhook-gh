namespace DotnetWebhookGH.Api.Configuration;

using DotnetWebhookGH.Api.Filters;
using Microsoft.AspNetCore.Mvc;

public static class FiltersConfiguration
{
    public static void AddApiFilters(this MvcOptions options)
    {
        options.Filters.Add(new PingFilter());
        options.Filters.Add(new RequestValidationFilter());
    }
}

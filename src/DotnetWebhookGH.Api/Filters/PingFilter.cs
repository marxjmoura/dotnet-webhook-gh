namespace DotnetWebhookGH.Api.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Mime;

public sealed class PingFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Request.Headers["X-GitHub-Event"] == "ping")
        {
            filterContext.Result = new ContentResult
            {
                Content = "pong",
                ContentType = MediaTypeNames.Text.Plain,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}

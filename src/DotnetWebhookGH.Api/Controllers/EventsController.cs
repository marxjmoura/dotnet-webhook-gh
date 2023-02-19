namespace DotnetWebhookGH.Api.Controllers;

using DotnetWebhookGH.Api.Data;
using DotnetWebhookGH.Api.Data.Model.Users;
using DotnetWebhookGH.Api.Payloads;
using DotnetWebhookGH.Api.Payloads.Events;
using DotnetWebhookGH.Api.Payloads.Issues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

[Route("{owner}/{repo}/issues/{number:int}/events")]
public class EventsController : Controller
{
    private readonly IDbContextFactory<ApiDbContext> _dbContextFactory;

    public EventsController(IDbContextFactory<ApiDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Get GitHub events
    /// </summary>
    /// <remarks>
    /// Get all events that occurred for an issue as they came from GitHub
    /// ([see payloads](https://docs.github.com/en/developers/webhooks-and-events/webhooks/webhook-events-and-payloads)).
    /// </remarks>
    /// <param name="routeParams">
    /// GitHub event payload.
    /// </param>
    /// <returns>
    /// Array of events.
    /// </returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IssueJson[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromRoute] EventRouteParams routeParams)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        var events = await dbContext.Issues
            .WhereNumber(routeParams.Number)
            .WhereRepositoryNameEqual(routeParams.Repo!)
            .WhereRepositoryOwnerEqual(routeParams.Owner!)
            .IncludeRepository()
            .IncludeSender()
            .IncludeAssignees()
            .IncludeReactions()
            .IncludeLabels()
            .ToListAsync();

        return Ok(events.Select(@event => @event.ToJson()).ToList());
    }
}

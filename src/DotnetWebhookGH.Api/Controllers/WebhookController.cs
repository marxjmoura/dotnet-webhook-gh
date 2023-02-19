namespace DotnetWebhookGH.Api.Controllers;

using DotnetWebhookGH.Api.Data;
using DotnetWebhookGH.Api.Features;
using DotnetWebhookGH.Api.Payloads.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

[Route("webhook/issues")]
public class WebhookController : Controller
{
    private readonly IDbContextFactory<ApiDbContext> _dbContextFactory;

    public WebhookController(IDbContextFactory<ApiDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Save GitHub event
    /// </summary>
    /// <remarks>
    /// Save the GitHub event (as it is) to the database.
    /// Currently, only issue events are supported.
    /// </remarks>
    /// <param name="payload">
    /// [GitHub payloads](https://docs.github.com/en/developers/webhooks-and-events/webhooks/webhook-events-and-payloads)
    /// </param>
    /// <returns>
    /// Does not return any content.
    /// </returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Save([FromBody] EventJson payload)
    {
        var @event = Request.Headers["X-GitHub-Event"];
        var delivery = Request.Headers["X-GitHub-Delivery"];

        using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        var snapshot = new IssueSnapshot(dbContext);
        await snapshot.SaveAsync(delivery, payload);

        return NoContent();
    }
}

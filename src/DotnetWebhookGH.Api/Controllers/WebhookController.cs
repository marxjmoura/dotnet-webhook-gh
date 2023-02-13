namespace DotnetWebhookGH.Api.Controllers;

using Amazon.DynamoDBv2;
using DotnetWebhookGH.Api.Data.DynamoDB;
using DotnetWebhookGH.Api.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Text.Json.Nodes;

[Route("webhook")]
public class WebhookController : Controller
{
    private readonly IAmazonDynamoDB _dynamoDB;

    public WebhookController(IAmazonDynamoDB dynamoDB)
    {
        _dynamoDB = dynamoDB;
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
    public async Task<IActionResult> Save([FromBody] JsonNode payload)
    {
        var @event = Request.Headers["X-GitHub-Event"];
        var delivery = Request.Headers["X-GitHub-Delivery"];

        if (@event == "ping")
        {
            return Content("pong");
        }

        if (@event != "issues")
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        var item = new DynamoDBItem(payload);
        var attributeMap = item.ToAttributeMap(@event, delivery);

        await _dynamoDB.PutItemAsync(DynamoDBTable.Name, attributeMap);

        return NoContent();
    }
}

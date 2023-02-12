namespace DotnetWebhookGH.Api.Controllers;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using DotnetWebhookGH.Api.Data.DynamoDB;
using DotnetWebhookGH.Api.Payloads;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json.Nodes;

[Route("{owner}/{repo}/issues/{number:int}/events")]
public class EventsController : Controller
{
    private readonly IAmazonDynamoDB _dynamoDB;

    public EventsController(IAmazonDynamoDB dynamoDB)
    {
        _dynamoDB = dynamoDB;
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
    [ProducesResponseType(typeof(JsonArray), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromRoute] EventRouteParams routeParams)
    {
        var response = await _dynamoDB.QueryAsync(new()
        {
            TableName = DynamoDBTable.Name,
            KeyConditions = routeParams.ToKeyConditions()
        });

        var events = response.Items
            .Select(item => Document.FromAttributeMap(item))
            .Select(document => JsonNode.Parse(document.ToJson()))
            .Select(@event => JsonNode.Parse(@event!["payload"]!.ToJsonString()) )
            .ToArray();

        return Ok(new JsonArray(events));
    }
}

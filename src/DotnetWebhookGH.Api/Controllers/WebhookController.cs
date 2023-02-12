namespace DotnetWebhookGH.Api.Controllers;

using Amazon.DynamoDBv2;
using DotnetWebhookGH.Api.Data.DynamoDB;
using DotnetWebhookGH.Api.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Nodes;

[Route("webhook")]
public class WebhookController : Controller
{
    private readonly IAmazonDynamoDB _dynamoDB;

    public WebhookController(IAmazonDynamoDB dynamoDB)
    {
        _dynamoDB = dynamoDB;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] JsonNode json)
    {
        var @event = Request.Headers["X-GitHub-Event"];

        if (@event == "ping")
        {
            return Ok("pong");
        }

        if (@event != "issues")
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }

        var item = new DynamoDBItem(json);
        var attributeMap = item.ToAttributeMap(@event);

        await _dynamoDB.PutItemAsync(DynamoDBTable.Name, attributeMap);

        return NoContent();
    }
}

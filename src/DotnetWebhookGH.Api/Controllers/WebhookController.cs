namespace DotnetWebhookGH.Api.Controllers;

using Amazon.DynamoDBv2;
using DotnetWebhookGH.Api.Data.DynamoDB;
using DotnetWebhookGH.Api.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

[Route("webhook")]
public class WebhookController : Controller
{
    private readonly IAmazonDynamoDB _dynamoDB;
    private readonly Dictionary<string, IModel> _modelMap;

    public WebhookController(IAmazonDynamoDB dynamoDB)
    {
        _dynamoDB = dynamoDB;
        _modelMap = new()
        {
            ["issues"] = new Issue()
        };
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] JsonNode json)
    {
        var @event = Request.Headers["X-GitHub-Event"];

        if (_modelMap.ContainsKey(@event))
        {
            var model = _modelMap[@event];
            var item = model.ToDynamoDBItem(json);

            await _dynamoDB.PutItemAsync(DynamoDBTable.Name, item);
        }

        return NoContent();
    }
}

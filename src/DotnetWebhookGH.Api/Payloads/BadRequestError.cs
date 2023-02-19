namespace DotnetWebhookGH.Api.Payloads;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class BadRequestError : IActionResult
{
    public BadRequestError(IEnumerable<ModelError> modelErrors)
    {
        Errors = modelErrors
            .Select(modelError => modelError.ErrorMessage)
            .ToList();
    }

    public IEnumerable<string> Errors { get; set; }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var json = new JsonResult(this) { StatusCode = 400 };
        await json.ExecuteResultAsync(context);
    }
}

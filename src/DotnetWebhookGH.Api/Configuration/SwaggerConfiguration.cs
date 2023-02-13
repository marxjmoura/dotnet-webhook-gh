namespace DotnetWebhookGH.Api.Configuration;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Nodes;

[ExcludeFromCodeCoverage]
public static class SwaggerConfiguration
{
    static string Title = ".NET Webhook for GitHub";
    static string Description = "Improve your workflow on GitHub. The sky's the limit!";
    static string Version = Environment.GetEnvironmentVariable("ASPNETCORE_API__Version")!;
    static string DocumentName = "docs";
    static string DocumentUrl = $"/{Version}/{DocumentName}/swagger.json";

    public static void AddApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(DocumentName, new OpenApiInfo
            {
                Title = Title,
                Version = Version,
                Description = Description
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            options.IncludeXmlComments(xmlPath);

            options.MapType<JsonNode>(() => new OpenApiSchema { Type = "object" });
            options.MapType<JsonArray>(() => new OpenApiSchema { Type = "array" });
        });
    }

    public static void UseApiDocumentation(this IApplicationBuilder api)
    {
        api.UsePathBase($"/{Version}");

        api.UseSwagger(options =>
        {
            options.RouteTemplate = "{documentName}/swagger.json";

            options.PreSerializeFilters.Add((swaggerDoc, request) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = $"{request.Scheme}://{request.Host.Value}/{Version}" }
                };
            });
        });

        api.UseSwaggerUI(options =>
        {
            options.DocumentTitle = Title;
            options.RoutePrefix = "swagger";
            options.SwaggerEndpoint(DocumentUrl, $"{Title} {Version}");
            options.DefaultModelsExpandDepth(-1);
            options.DocExpansion(DocExpansion.List);
        });

        api.UseReDoc(options =>
        {
            options.DocumentTitle = Title;
            options.RoutePrefix = "docs";
            options.SpecUrl = DocumentUrl;
        });
    }
}

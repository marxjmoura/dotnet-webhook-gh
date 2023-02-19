using Amazon.SecretsManager;
using DotnetWebhookGH.Api.Configuration;
using DotnetWebhookGH.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

builder.Services.AddAWSService<IAmazonSecretsManager>();
builder.Services.AddDbContextFactory<ApiDbContext, ApiDbContextFactory>();

builder.Services
    .AddControllers(options => options.AddApiFilters())
    .AddJsonOptions(options => options.AddApiJsonSerializerOptions());

builder.Services.AddLogging();
builder.Services.AddApiDocumentation();

var api = builder.Build();

var loggerFactory = api.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddLambdaLogger(builder.Configuration.GetLambdaLoggerOptions("Logging"));

api.UseApiDocumentation();
api.UseRouting();
api.UseEndpoints(endpoints => endpoints.MapControllers());

api.Run();

using Amazon.DynamoDBv2;
using DotnetWebhookGH.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddApiDocumentation();

var api = builder.Build();

var loggerFactory = api.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddLambdaLogger(builder.Configuration.GetLambdaLoggerOptions("Logging"));

api.UseApiDocumentation();
api.UseRouting();
api.UseEndpoints(endpoints => endpoints.MapControllers());

api.Run();

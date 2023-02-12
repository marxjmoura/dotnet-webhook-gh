using Amazon.DynamoDBv2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();

builder.Services.AddControllers();
builder.Services.AddLogging();

var api = builder.Build();

var loggerFactory = api.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddLambdaLogger(builder.Configuration.GetLambdaLoggerOptions("Logging"));

api.UseRouting();
api.UseEndpoints(endpoints => endpoints.MapControllers());

api.Run();

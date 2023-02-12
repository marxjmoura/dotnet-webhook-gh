using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using NSubstitute;
using Amazon.DynamoDBv2;

public static class TestProgram
{
    public static TestServer CreateServer()
    {
        var host = new WebHostBuilder()
            .UseEnvironment("Testing")
            .ConfigureServices(services =>
            {
                services.AddLogging();
                services.AddControllers().AddApplicationPart(Assembly.Load("DotnetWebhookGH.Api"));
                services.AddSingleton(Substitute.For<IAmazonDynamoDB>());
            })
            .Configure(api =>
            {
                api.UseRouting();
                api.UseEndpoints(endpoints => endpoints.MapControllers());
            });

        return new(host);
    }
}

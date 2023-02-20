using Amazon.SecretsManager;
using DotnetWebhookGH.Api.Configuration;
using DotnetWebhookGH.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using System;
using System.Reflection;

public static class TestProgram
{
    public static TestServer CreateServer()
    {
        var host = new WebHostBuilder()
            .UseEnvironment("Testing")
            .ConfigureServices(services =>
            {
                services.AddLogging();

                services
                    .AddControllers(options => options.AddApiFilters())
                    .AddJsonOptions(options => options.AddApiJsonSerializerOptions())
                    .AddApplicationPart(Assembly.Load("DotnetWebhookGH.Api"));

                services.AddDbContextFactory<ApiDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });

                services.AddSingleton<ApiDbContext>();

                services.AddSingleton(Substitute.For<IAmazonSecretsManager>());
            })
            .Configure(api =>
            {
                api.UseRouting();
                api.UseEndpoints(endpoints => endpoints.MapControllers());
            });

        return new(host);
    }
}

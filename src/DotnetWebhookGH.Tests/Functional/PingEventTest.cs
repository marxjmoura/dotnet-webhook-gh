namespace DotnetWebhookGH.Tests.Functional;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class PingEventTest
{
    [Fact]
    public async Task ShouldRespondPong()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        client.DefaultRequestHeaders.Add("X-GitHub-Event", "ping");

        var fixture = Path.Combine(AppContext.BaseDirectory, "Fixtures/Ping.json");
        var requestJson = await File.ReadAllTextAsync(fixture);
        var request = new StringContent(requestJson, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("/webhook/issues", request);
        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("pong", responseContent);
    }
}

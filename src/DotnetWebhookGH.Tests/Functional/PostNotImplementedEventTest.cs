namespace DotnetWebhookGH.Tests.Functional;

using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class PostNotImplementedEventTest
{
    [Fact]
    public async Task ShouldReturn501()
    {
        var server = TestProgram.CreateServer();
        var client = server.CreateClient();

        client.DefaultRequestHeaders.Add("X-GitHub-Event", "other");

        var request = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync("/webhook", request);

        Assert.Equal(HttpStatusCode.NotImplemented, response.StatusCode);
    }
}

using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using System.IO;

public class ConsumerPactTest : IDisposable
{
    private readonly WireMockServer _server;
    private readonly HttpClient _client;
    private readonly string _pactFilePath = @"C:\Users\tasso\source\repos\ConsumerService\ConsumerService.Tests\Pacts\consumer-provider-pact.json";

    public ConsumerPactTest()
    {
        // Ξεκινάμε έναν τοπικό mock server
        _server = WireMockServer.Start(port: 9222);

        // Ορίζουμε τη mock διαδρομή για το /discount
        _server.Given(
            Request.Create().WithPath("/discount").UsingGet()
        )
        .RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody("{\"customerRating\": 4.5, \"amountToDiscount\": 0.45}")
        );

        _client = new HttpClient { BaseAddress = new Uri("http://localhost:9222") };
    }

    [Fact]
    public async Task ItHandlesValidResponse()
    {
        var response = await _client.GetAsync("/discount");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(content);

        Assert.Equal(4.5, jsonResponse.GetProperty("customerRating").GetDouble());
        Assert.Equal(0.45, jsonResponse.GetProperty("amountToDiscount").GetDouble());

        SavePactFile();
    }

    private void SavePactFile()
    {
        var pact = new
        {
            consumer = new { name = "ConsumerService" },
            provider = new { name = "ProviderService" },
            interactions = new[]
            {
                new
                {
                    description = "A valid request for discount",
                    request = new
                    {
                        method = "GET",
                        path = "/discount"
                    },
                    response = new
                    {
                        status = 200,
                        headers = new
                        {
                            Content_Type = "application/json"
                        },
                        body = new
                        {
                            customerRating = 4.5,
                            amountToDiscount = 0.45
                        }
                    }
                }
            },
            metadata = new
            {
                pactSpecification = new
                {
                    version = "2.0.0"
                }
            }
        };

        var pactJson = JsonSerializer.Serialize(pact, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_pactFilePath, pactJson);
    }

    public void Dispose()
    {
        _server.Stop();
    }
}

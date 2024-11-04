using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using PactNet;

namespace ProviderService.Tests
{
    public class ProviderPactTest : IDisposable
    {
        private readonly IHost _providerHost;

        public ProviderPactTest()
        {
            // Εκκίνηση του ProviderService μέσα από το test
            _providerHost = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<ProviderService.Startup>() // Χρήση του ProviderService
                              .UseUrls("http://localhost:5001");    // Ορισμός της πόρτας για τον provider
                })
                .Build();

            _providerHost.Start(); // Εκκίνηση του host
        }

        [Fact]
        public void EnsureProviderHonoursPactWithConsumer()
        {
            // Ορισμός διαδρομής στο Pact αρχείο
            string pactFile = @"C:\Users\tasso\source\repos\ConsumerService\ConsumerService.Tests\Pacts\consumer-provider-pact.json";

            // Ρυθμίσεις για τον Pact Verifier
            var config = new PactVerifierConfig
            {
                Verbose = true // Εμφάνιση λεπτομερών πληροφοριών κατά την επαλήθευση
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("ProviderService", "http://localhost:5001") // Χρήση του σωστού URL
                .HonoursPactWith("ConsumerService")
                .PactUri(pactFile) // Παροχή διαδρομής στο Pact αρχείο
                .Verify();
        }

        public void Dispose()
        {
            // Τερματισμός του Provider μετά το test
            _providerHost.Dispose();
        }
    }
}

using System;
using System.IO;
using PactNet;
using PactNet.Verifier;
using Xunit;
using System.Collections.Generic;
using PactNet.Infrastructure.Outputters;

public class ProviderPactTest
{
    [Fact]
    public void EnsureProviderHonoursPactWithConsumer()
    {
        var config = new PactVerifierConfig
        {
            Outputters = new List<IOutput>
            {
                new ConsoleOutput()  // Χρήση του ConsoleOutput για την καταγραφή εξόδου
            }
        };

        // Δημιουργούμε το PactVerifier
        IPactVerifier pactVerifier = new PactVerifier(config);

        // Επιβεβαιώνουμε ότι ο πάροχος τιμά το συμβόλαιο με τον καταναλωτή
        pactVerifier
            .ServiceProvider("ProviderService", new Uri("http://localhost:5001"))
            .WithFileSource(new FileInfo(@"C:\Users\tasso\source\repos\ConsumerService\ConsumerService.Tests\Pacts\consumer-provider-pact.json")) // Φόρτωση του Pact αρχείου
            .Verify();
    }
}

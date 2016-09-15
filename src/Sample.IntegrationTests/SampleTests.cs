using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Sample.IntegrationTests
{
    public abstract class SampleTests
    {
        protected HttpClient Client { get; }

        protected SampleTests()
        {
            // Setup a test server
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            Client = server.CreateClient();            // Given a test server
        }
    }
}

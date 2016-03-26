using System;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Sample.IntegrationTests
{
    public abstract class SampleTests
    {
        private readonly Action<IApplicationBuilder> _app;
        private readonly Action<IServiceCollection> _services;

        protected SampleTests()
        {
            var startup = new Startup(new HostingEnvironment());
            _app = startup.Configure;
            _services = startup.ConfigureServices;
        }

        public HttpClient SampleClient()
        {
            // Given a test server
            var server = TestServer.Create(_app, _services);
            return server.CreateClient();
        }
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Sample.IntegrationTests.Commands
{
    public class SayHelloTests: SampleTests
    {
        [Fact]
        public async Task Get_hello_should_ignore_bob()
        {
            // Given a sample client
            var client = SampleClient();

            // When I GET /hello/bob
            var response = await client.GetAsync("/hello/bob");

            // Then bob should be ignored
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("\"I don't want to talk to you bob\"");
        }

        [Fact]
        public async Task Get_hello_should_say_hello_to_Sue()
        {
            // Given a sample client
            var client = SampleClient();

            // When I GET /hello/Sue
            var response = await client.GetAsync("/hello/Sue");

            // Then bob should be ignored
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("\"Hello Sue\"");
        }
    }
}

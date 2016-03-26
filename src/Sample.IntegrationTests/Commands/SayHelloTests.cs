using System;
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
        public void Get_hello_should_ignore_bob()
        {
            // Given a sample client
            var client = SampleClient();

            // When I GET /hello/bob
            var response = client.GetAsync("/hello/bob").Result;

            // Then bob should be ignored
            string result = response.Content.ReadAsStringAsync().Result;
            result.Should().Be("\"I don't want to talk to you bob\"");
        }

        [Fact]
        public void Get_hello_should_say_hello_to_Sue()
        {
            // Given a sample client
            var client = SampleClient();

            // When I GET /hello/Sue
            var response = client.GetAsync("/hello/Sue").Result;

            // Then bob should be ignored
            string result = response.Content.ReadAsStringAsync().Result;
            result.Should().Be("\"Hello Sue\"");
        }
    }
}

using System;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Sample.IntegrationTests.CommandSets
{
    public class AccountCommandsTests: SampleTests
    {
        [Fact]
        public void Get_hello_should_ignore_bob()
        {
            // Given a sample client
            var client = SampleClient();

            // When I call POST /account/signin
            var value = new StringContent("");
            var response = client.PostAsync("/account/signin", value).Result;

            // Then the result should be Hello
            string result = response.Content.ReadAsStringAsync().Result;
            result.Should().Be("\"Hello\"");
        }

        [Fact]
        public void Get_hello_should_say_hello_to_Sue()
        {
            // Given a sample client
            var client = SampleClient();

            // When I vall Delete /account/signout
            var response = client.DeleteAsync("/account/signout").Result;

            // Then the result should be Goodbye
            string result = response.Content.ReadAsStringAsync().Result;
            result.Should().Be("\"Goodbye\"");
        }
    }
}

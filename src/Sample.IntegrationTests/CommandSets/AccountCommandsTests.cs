using System;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task Get_hello_should_ignore_bob()
        {
            // Given a sample client
            var client = SampleClient();

            // When I call POST /account/signin
            var value = new StringContent("");
            var response = await client.PostAsync("/account/signin", value);

            // Then the result should be Hello
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("\"Hello\"");
        }

        [Fact]
        public async Task Get_hello_should_say_hello_to_Sue()
        {
            // Given a sample client
            var client = SampleClient();

            // When I vall Delete /account/signout
            var response = await client.DeleteAsync("/account/signout");

            // Then the result should be Goodbye
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("\"Goodbye\"");
        }
    }
}

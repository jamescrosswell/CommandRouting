using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Sample.IntegrationTests.Commands
{
    public class SayHelloTests: SampleTests
    {
        [Fact]
        public async Task Get_hello_should_ignore_bob()
        {
            // When I GET /hello/bob
            var response = await Client.GetAsync("/hello/bob");

            // Then bob should be ignored
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Be("I don't want to talk to you bob");
        }

        [Fact]
        public async Task Get_hello_should_say_hello_to_Sue()
        {
            // When I GET /hello/Sue
            var response = await Client.GetAsync("/hello/Sue");

            // Then bob should be ignored
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Be("Hello Sue");
        }
    }
}

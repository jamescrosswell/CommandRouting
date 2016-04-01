using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Sample.IntegrationTests.Commands
{
    public class JumpTests : SampleTests
    {
        [Fact]
        public async Task When_I_say_jump_dont_ask_how_high()
        {
            // Given a sample client
            var client = SampleClient();

            // When I call POST /account/signin
            var response = await client.GetAsync("/jump");

            // Then the result should be Hello
            string result = await response.Content.ReadAsStringAsync();
            result.Should().Be("\"2.4 meters, yes sir!\"");
        }
    }
}

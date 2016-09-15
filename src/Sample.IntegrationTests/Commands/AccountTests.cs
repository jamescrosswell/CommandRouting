using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Sample.IntegrationTests.Commands
{
    public class AccountCommandsTests: SampleTests
    {
        [Fact]
        public async Task Get_hello_should_ignore_bob()
        {
            // When I call POST /account/signin
            var value = new StringContent("");
            var response = await Client.PostAsync("/account/signin", value);

            // Then the result should be Hello
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Be("Hello");
        }

        [Fact]
        public async Task Get_hello_should_say_hello_to_Sue()
        {
            // When I vall Delete /account/signout
            var response = await Client.DeleteAsync("/account/signout");

            // Then the result should be Goodbye
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Be("Goodbye");
        }
    }
}

using CommandRouting.Helpers;
using FluentAssertions;
using Xunit;

namespace CommandRouting.UnitTests.Helpers
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData("Foo")]
        [InlineData("Foobar")]
        public void ForceTrailing_should_append_suffix_when_not_present(string original)
        {
            // When I force a trailing bar
            string result = original.ForceTrailing("bar");

            // Then the result should be Foobar
            result.Should().Be("Foobar");
        }

        [Theory]
        [InlineData("Foobar")]
        [InlineData("bar")]
        public void ForceLeading_should_add_prefix_when_not_present(string original)
        {
            // When I force a leading Foo
            string result = original.ForceLeading("Foo");

            // Then the result should be Foobar
            result.Should().Be("Foobar");
        }        

        [Theory]
        [InlineData("Foobar")]
        [InlineData("Foo")]
        public void StripTrailing_should_remove_suffix_when_present(string original)
        {
            // When I strip a trailing bar
            string result = original.StripTrailing("bar");

            // Then the result should be Foo
            result.Should().Be("Foo");
        }

        [Theory]
        [InlineData("Foobar")]
        [InlineData("bar")]
        public void StripLeading_should_remove_prefix_when_present(string original)
        {
            // When I strip a leading Foo
            string result = original.StripLeading("Foo");

            // Then the result should be bar
            result.Should().Be("bar");
        }

        // Strip
        [Theory]
        [InlineData("SueClothes")]
        [InlineData("ClothesSue")]
        [InlineData("ClothesSueClothes")]
        public void Strip_should_remove_suffix_and_prefix(string original)
        {
            // When I strip the Clothes off Sue
            string result = original.Strip("Clothes");

            // Then the result should just be Sue
            result.Should().Be("Sue");
        }

    }
}

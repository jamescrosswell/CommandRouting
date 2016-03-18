using CommandRouting.Router;
using FluentAssertions;
using Xunit;

namespace CommandRouting.UnitTests.Helpers
{
    public class ReflectionHelperTests
    {
        public class NestedEntity
        {
            public string Bar { get; set; }
            public int Rain { get; set; }
        }

        public class ComplexEntity
        {
            public decimal Top { get; set; }
            public NestedEntity Nest { get; set; }
        }

        [Fact]
        public void TrySetDeepPropertyStringValue_should_set_top_level_properties()
        {
            // Given a complex type with nested properties
            ComplexEntity entity = new ComplexEntity()
            {
                Nest = new NestedEntity()
            };

            // When I set a top level property
            entity.TryParseDeepPropertyValue("Top", "10");

            // Then the property should be set correctly
            Assert.Equal(10m, entity.Top);
        }

        delegate object PropertySelector(ComplexEntity entity);

        [Fact]
        public void TrySetDeepPropertyStringValue_should_set_nested_properties()
        {
            // Given a complex type with nested properties
            ComplexEntity entity = new ComplexEntity()
            {
                Nest = new NestedEntity()
            };

            // When I set a top level property
            entity.TryParseDeepPropertyValue("Nest.Bar", "Rain");

            // Then the property should be set correctly
            Assert.Equal("Rain", entity.Nest.Bar);
        }

        [Fact]
        public void TrySetDeepPropertyStringValue_should_ignore_case()
        {
            // Given a complex type with nested properties
            ComplexEntity entity = new ComplexEntity()
            {
                Nest = new NestedEntity()
            };

            // When I set a top level property
            entity.TryParseDeepPropertyValue("nest.rain", "42");

            // Then the property should be set correctly
            Assert.Equal(42, entity.Nest.Rain);
        }

        [Fact]
        public void TrySetDeepPropertyStringValue_should_ignore_nonexistent_properties()
        {
            // Given a complex type with nested properties
            const decimal top = 99m;
            const string bar = "The answer";
            const int rain = 42;
            ComplexEntity entity = new ComplexEntity()
            {
                Top = top,
                Nest = new NestedEntity
                {
                    Bar = bar,
                    Rain = rain
                }
            };

            // When I set a top level property
            bool result = entity.TryParseDeepPropertyValue("nest.wtf", "42");

            // Then SetDeepPropertyValue should return false
            result.Should().BeFalse();

            // And none of the original property values should be changed
            entity.Top.Should().Be(top);
            entity.Nest.Bar.Should().Be(bar);
            entity.Nest.Rain.Should().Be(rain);
        }
    }
}

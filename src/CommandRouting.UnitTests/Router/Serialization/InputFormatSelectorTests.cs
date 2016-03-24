using System;
using System.Collections.Generic;
using CommandRouting.Router.Serialization;
using FluentAssertions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using NSubstitute;
using Xunit;

namespace CommandRouting.UnitTests.Router.Serialization
{
    public class InputFormatSelectorTests
    {
        readonly Func<bool, IInputFormatter> _canTheContext = (canRead) =>
        {
            var formatter = Substitute.For<IInputFormatter>();
            formatter.CanRead(Arg.Any<InputFormatterContext>()).Returns(canRead);
            return formatter;
        };

        [Fact]
        public void InputFormatSelector_should_return_the_first_formatter_if_no_formatters_match()
        {
            // Given an  input format context
            HttpContext httpContext = Substitute.For<HttpContext>();
            InputFormatterContext formatContext = httpContext.InputFormatterContext<string>();

            // And a bunch of formatters that can't read the context
            IInputFormatter firstFormatter = _canTheContext(false);
            IInputFormatter secondFormatter = _canTheContext(false);
            IInputFormatter thirdFormatter = _canTheContext(false);
            List<IInputFormatter> inputFormatters = new List<IInputFormatter>
            {
                firstFormatter, secondFormatter, thirdFormatter
            };

            // When I try to get the best formatter
            IInputFormatter result = inputFormatters.GetBestFormatter(formatContext);

            // Then the result should be the first formatter
            result.Should().Be(firstFormatter);
        }

        [Fact]
        public void InputFormatSelector_should_return_the_best_formatter()
        {
            // Given an  input format context
            HttpContext httpContext = Substitute.For<HttpContext>();
            InputFormatterContext formatContext = httpContext.InputFormatterContext<string>();

            // And a bunch of formatters, with a couple that can read the context
            IInputFormatter firstFormatter = _canTheContext(false);
            IInputFormatter secondFormatter = _canTheContext(true);
            IInputFormatter thirdFormatter = _canTheContext(true);
            IInputFormatter fourthFormatter = _canTheContext(false);
            List<IInputFormatter> inputFormatters = new List<IInputFormatter>
            {
                firstFormatter, secondFormatter, thirdFormatter, fourthFormatter
            };

            // When I try to get the best formatter
            IInputFormatter result = inputFormatters.GetBestFormatter(formatContext);

            // Then the result should be the first formatter that can read the context
            result.Should().Be(secondFormatter);
        }
    }
}

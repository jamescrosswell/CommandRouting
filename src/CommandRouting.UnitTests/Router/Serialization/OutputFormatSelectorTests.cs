using System;
using System.Collections.Generic;
using CommandRouting.Handlers;
using CommandRouting.Router.Serialization;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using NSubstitute;
using Xunit;

namespace CommandRouting.UnitTests.Router.Serialization
{
    public class OutputFormatSelectorTests
    {
        readonly Func<bool, IOutputFormatter> _canTheContext = (canRead) =>
        {
            var formatter = Substitute.For<IOutputFormatter>();
            formatter.CanWriteResult(Arg.Any<OutputFormatterWriteContext>()).Returns(canRead);
            return formatter;
        };

        [Fact]
        public void OutputFormatSelector_should_return_the_first_formatter_if_no_formatters_match()
        {
            // Given an  output format context
            HttpContext httpContext = Substitute.For<HttpContext>();
            IHandlerResult handlerResult = new Handled();
            OutputFormatterWriteContext formatContext = httpContext.OutputFormatterContext(handlerResult);

            // And a bunch of formatters that can't read the context
            IOutputFormatter firstFormatter = _canTheContext(false);
            IOutputFormatter secondFormatter = _canTheContext(false);
            IOutputFormatter thirdFormatter = _canTheContext(false);
            List<IOutputFormatter> outputFormatters = new List<IOutputFormatter>
            {
                firstFormatter, secondFormatter, thirdFormatter
            };

            // When I try to get the best formatter
            IOutputFormatter result = outputFormatters.GetBestFormatter(formatContext);

            // Then the result should be the first formatter
            result.Should().Be(firstFormatter);
        }

        [Fact]
        public void OutputFormatSelector_should_return_the_best_formatter()
        {
            // Given an  output format context
            HttpContext httpContext = Substitute.For<HttpContext>();
            IHandlerResult handlerResult = new Handled();
            OutputFormatterWriteContext formatContext = httpContext.OutputFormatterContext(handlerResult);

            // And a bunch of formatters, with a couple that can write to the context
            IOutputFormatter firstFormatter = _canTheContext(false);
            IOutputFormatter secondFormatter = _canTheContext(true);
            IOutputFormatter thirdFormatter = _canTheContext(true);
            IOutputFormatter fourthFormatter = _canTheContext(false);
            List<IOutputFormatter> outputFormatters = new List<IOutputFormatter>
            {
                firstFormatter, secondFormatter, thirdFormatter, fourthFormatter
            };

            // When I try to get the best formatter
            IOutputFormatter result = outputFormatters.GetBestFormatter(formatContext);

            // Then the result should be the first formatter that can write to the context
            result.Should().Be(secondFormatter);
        }
    }
}

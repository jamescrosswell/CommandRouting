using CommandRouting.Helpers;
using CommandRouting.Router.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace CommandRouting.UnitTests.Router.Serialization
{
    public class RequestReaderTests
    {
        class Foo
        {
            
        }

        [Fact]
        public void DeserializeRequestAsync_should_delegate_reading_to_the_appropriate_input_formatter()
        {
            // Given a dummy input format and selector
            var inputFormatter = Substitute.For<IInputFormatter>();
            var inputFormatterResult = InputFormatterResult.SuccessAsync(new Foo());
            inputFormatter
                .ReadAsync(Arg.Any<InputFormatterContext>())
                .Returns(inputFormatterResult);
            var options = Substitute.For<IOptions<MvcOptions>>();
            options.Value.Returns(new MvcOptions
            {
                InputFormatters = { inputFormatter }
            });

            // When I deserialize a request
            var httpContext = new DefaultHttpContext();
            var requestReader = new RequestReader(options);
            var result = requestReader.DeserializeRequestAsync<Foo>(httpContext).Result;

            // Then the input formatter should be used to read the model
            inputFormatter.Received(1).ReadAsync(Arg.Any<InputFormatterContext>());
        }
    }
}

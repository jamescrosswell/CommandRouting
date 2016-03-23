using System.Collections.Generic;
using System.Threading.Tasks;
using CommandRouting.Router;
using CommandRouting.Router.Serialization;
using CommandRouting.Router.ValueParsers;
using FluentAssertions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using NSubstitute;
using Xunit;

namespace CommandRouting.UnitTests.Router
{
    public class RequestModelActivatorTests
    {
        public class Foo
        {
            public string Name { get; set; }
            public int Ranking { get; set; }
        }

        [Fact]
        public void CreateRequestModel_should_merge_message_body_and_route_data_to_create_a_command_request_model()
        {
            // Given a route context with a json message in the body and some route data
            HttpContext httpContext = Substitute.For<HttpContext>(); 
            RouteContext routeContext = new RouteContext(httpContext);

            // And a request reader that returns a partial foo (with the name set)
            IRequestReader requestReader = Substitute.For<IRequestReader>();
            Foo readerModel = new Foo {Name = "Bar"};
            requestReader
                .DeserializeRequestAsync<Foo>(Arg.Any<HttpContext>())
                .Returns(Task.FromResult(readerModel));

            // And a value parser that sets the ranking property
            IValueParser valueParser = Substitute.For<IValueParser>();
            valueParser
                .When(x => x.ParseValues(Arg.Any<RouteData>(), Arg.Any<Foo>()))
                .Do(x => { x.Arg<Foo>().Ranking = 42;});
            IEnumerable<IValueParser> valueParsers = new List<IValueParser> { valueParser };

            // When I try to activate a request model
            RequestModelActivator modelActivator = new RequestModelActivator(requestReader, valueParsers);
            var result = modelActivator.CreateRequestModelAsync<Foo>(routeContext).Result;

            // Then the result should be an instance of Foo with all of it's properties set correctly
            // from a combination of the requestReader and the valueParser
            result?.Name.Should().Be("Bar");
            result?.Ranking.Should().Be(42);
        }
    }
}

using CommandRouting.Router.ValueParsers;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace CommandRouting.UnitTests.Router.ValueParsers
{
    public class RouteValueParserTets
    {
        class Child
        {
            public string Name { get; set; }
            public Parent Mum { get; set; } = new Parent();
            public Parent Dad { get; set; } = new Parent();
        }

        class Parent
        {
            public string Name { get; set; }
            public decimal Height { get; set; }
        }

        [Fact]
        public void ParseValues_sets_properties_on_request_models()
        {
            // Given some route data
            RouteData routeData = new RouteData();
            routeData.Values.Add("name", "Willy");
            routeData.Values.Add("mum.Name", "Sally");
            routeData.Values.Add("mum.Height", "140.5");
            routeData.Values.Add("Dad.name", "Bob");
            routeData.Values.Add("dad.height", "205.3");

            // And an empty request model
            Child requestModel = new Child();

            // When I try to parse the values
            RouteValueParser parser = new RouteValueParser();
            parser.ParseValues(routeData, requestModel);

            // Then the properties on the request model should be set correctly
            requestModel.Name.Should().Be("Willy");
            requestModel.Mum.Name.Should().Be("Sally");
            requestModel.Mum.Height.Should().Be(140.5m);
            requestModel.Dad.Name.Should().Be("Bob");
            requestModel.Dad.Height.Should().Be(205.3m);
        }
    }
}

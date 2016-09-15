using System;
using CommandRouting.Handlers;
using CommandRouting.Router;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CommandRouting.UnitTests.Router
{
    public class CommandPipelineTests
    {
        public class FooRequest
        {
        }

        public class TestFooHandler : QueryHandler<FooRequest, int>
        {
            public override HandlerResult Dispatch(FooRequest request)
            {
                return Handled(42);
            }
        }

        [Theory]
        [InlineData("GET", false)]
        [InlineData("POST", true)]
        public void CanHandleVerb_should_match_appropriate_request_methods(string requestMethod, bool expectedResult)
        {
            // Given an HttpContext
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Method = requestMethod;

            // When I check if the pipeline can handle the request
            CommandPipeline<FooRequest> pipeline = new CommandPipeline<FooRequest>(HttpVerb.Post, new TestFooHandler());
            var result = pipeline.CanHandleVerb(httpContext);

            // Then the pipeline should only handle appropriate verbs
            result.Should().Be(expectedResult);
        }

    }
}

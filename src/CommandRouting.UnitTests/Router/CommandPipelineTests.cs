using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandRouting.Handlers;
using CommandRouting.Router;
using CommandRouting.Router.ValueParsers;
using FluentAssertions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Routing;
using Xunit;

namespace CommandRouting.UnitTests.Router
{
    public class CommandPipelineTests
    {
        public class FooRequest
        {
        }

        public class TestFooHandler : CommandHandler<FooRequest, int>
        {
            public override HandlerResult Dispatch(FooRequest request)
            {
                return Handled(42);
            }
        }

        [Theory]
        [InlineData("GET", typeof(NotHandled))]
        [InlineData("POST", typeof(Handled<int>))]
        public void Dispatch_should_only_handle_appropriate_request_methods(string requestMethod, Type expectedResultType)
        {
            // Given a POST FooRequest CommandPipeline
            CommandPipeline<FooRequest> pipeline = new CommandPipeline<FooRequest>(HttpVerb.Post, new TestFooHandler());

            // And an HttpContext for a GET operation
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Method = requestMethod;

            // When I try to dispatch a FooRequest
            FooRequest request = new FooRequest();
            IHandlerResult handlerResult = pipeline.Dispatch(httpContext, request);

            // Then the pipeline should not handle the request
            handlerResult.Should().BeOfType(expectedResultType);
        }

    }
}

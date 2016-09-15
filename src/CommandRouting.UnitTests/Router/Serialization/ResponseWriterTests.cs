using System.Buffers;
using System.Collections.Generic;
using System.Net;
using CommandRouting.Handlers;
using CommandRouting.Router.Serialization;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;
using CommandRouting.Helpers;

namespace CommandRouting.UnitTests.Router.Serialization
{
    public class ResponseWriterTests
    {
        public class Foo
        {
            public string Name { get; set; }
            public int Ranking { get; set; }
        }

        private static IOptions<MvcOptions> Options()
        {
            var jsonFormatter = new JsonOutputFormatter(
                new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                },
                ArrayPool<char>.Shared
            );

            var options = Substitute.For<IOptions<MvcOptions>>();
            options.Value.Returns(new MvcOptions
            {
                OutputFormatters = { jsonFormatter }
            });
            return options;
        }


        [Fact]
        public void SerializeResponse_should_set_status_code_from_IHttpResponse()
        {
            // Given an HttpContext 
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = "application/json";

            // And and IHttpResponse
            IHandlerResult handlerResult = new Handlers.HttpResponse(HttpStatusCode.BadRequest);

            // When I try to write the response out to the http context
            ResponseWriter responseWriter = new ResponseWriter(Options());
            responseWriter.SerializeResponseAsync(handlerResult, httpContext).Wait();

            // Then the status code of the HttpReponse should be set correctly
            httpContext.Response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
        }

        [Fact]
        public void SerializeResponse_should_assume_status_code_200_for_non_IHttpResponse()
        {
            // Given an HttpContext 
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = "application/json";

            // And a Handled result
            IHandlerResult handlerResult = new Handled();

            // When I try to write the response out to the http context
            ResponseWriter responseWriter = new ResponseWriter(Options());
            responseWriter.SerializeResponseAsync(handlerResult, httpContext).Wait();


            // Then the status code of the HttpReponse should be set correctly
            httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}

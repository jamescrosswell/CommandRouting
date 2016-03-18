using System.IO;
using System.Net;
using CommandRouting.Handlers;
using CommandRouting.Router.Serialization;
using FluentAssertions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;
using Xunit;

namespace CommandRouting.UnitTests.Router.Serialization
{
    public class ResponseWriterTests
    {
        public class Foo
        {
            public string Name { get; set; }
            public int Ranking { get; set; }
        }

        private static IOutputFormatter OutputFormatter => new JsonOutputFormatter(
                new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                }
            );

        [Fact]
        public void SerializeResponse_should_set_status_code_from_IHttpResponseResult()
        {
            // Given an HttpContext 
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = "application/json";

            // And and IHttpResponseResult
            IHandlerResult handlerResult = new HttpResponseResult(HttpStatusCode.BadRequest);

            // When I try to write the response out to the http context
            ResponseWriter responseWriter = new ResponseWriter(httpContext, OutputFormatter);
            responseWriter.SerializeResponse(handlerResult);


            // Then the status code of the HttpReponse should be set correctly
            httpContext.Response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
        }

        [Fact]
        public void SerializeResponse_should_assume_status_code_200_for_non_IHttpResponseResult()
        {
            // Given an HttpContext 
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = "application/json";

            // And a Handled result
            IHandlerResult handlerResult = new Handled();

            // When I try to write the response out to the http context
            ResponseWriter responseWriter = new ResponseWriter(httpContext, OutputFormatter);
            responseWriter.SerializeResponse(handlerResult);


            // Then the status code of the HttpReponse should be set correctly
            httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void SerializeResponse_should_serialize_the_response_object_to_the_message_body()
        {
            // Given an HttpContext 
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.ContentType = "application/json";

            // And an Handled result
            Foo foo = new Foo()
            {
                Name = "Bar",
                Ranking = 42
            };
            IHandlerResult handlerResult = new Handled<Foo>(foo);

            // When I try to write the response out to the http context
            ResponseWriter responseWriter = new ResponseWriter(httpContext, OutputFormatter);
            responseWriter.SerializeResponse(handlerResult);


            // Then the status code of the HttpReponse should be OK
            httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.OK);

            // And the command response should have been written to the response stream
            string resultText;
            httpContext.Response.Body.Position = 0;
            using (var reader = new StreamReader(httpContext.Response.Body))
            {
                resultText = reader.ReadToEnd();
            }
            resultText.Should().Be("{ name: \"Bar\", ranking: 42 }");

        }
    }
}

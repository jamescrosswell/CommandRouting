using System.IO;
using System.Text;
using CommandRouting.Router;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Xunit;

namespace CommandRouting.UnitTests.Router
{
    public class RequestModelActivatorTests
    {
        public class Foo
        {
            public string Name { get; set; }
        }

        [Fact]
        public void CreateRequestModel_should_create_a_command_request_model_from_the_http_request_body()
        {
            // Given an HttpRequest with a json message in the body
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "POST";
            httpContext.Request.ContentType = "application/json";
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{ name: 'Bar' }"));            

                // When I try to bind to a request model
            RequestModelActivator modelParser = new RequestModelActivator(httpContext);
            object requestModel = modelParser.CreateRequestModel(typeof (Foo));

            // Then the result should be an instance of Foo with all of it's properties set correctly
            Foo result = requestModel as Foo;
            Assert.Equal("Bar", result?.Name);
        }
    }
}

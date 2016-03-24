using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CommandRouting.Handlers;
using CommandRouting.Helpers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;

namespace CommandRouting.Router.Serialization
{
    public interface IResponseWriter
    {
        /// <summary>
        /// Creates an appropriate HttpResponse from our command handler result
        /// </summary>
        /// <param name="handlerResult">The command handler result that we need to create ah HttpResponse from</param>
        /// <param name="httpContext">The http context where the response will be written</param>
        Task SerializeResponseAsync(IHandlerResult handlerResult, HttpContext httpContext);
    }

    internal class ResponseWriter : IResponseWriter
    {
        private readonly IEnumerable<IOutputFormatter> _outputFormatters;

        public ResponseWriter(IEnumerable<IOutputFormatter> outputFormatters)
        {
            Ensure.NotNull(outputFormatters, nameof(outputFormatters));
            _outputFormatters = outputFormatters;
        }

        /// <inheritdoc />
        public async Task SerializeResponseAsync(IHandlerResult handlerResult, HttpContext httpContext)
        {
            // Validate the arguments
            Ensure.NotNull(handlerResult, nameof(handlerResult));
            Ensure.NotNull(httpContext, nameof(httpContext));

            // Set the status code on the response
            var httpResponse = handlerResult as IHttpResponse;
            var status = httpResponse?.Status ?? HttpStatusCode.OK;
            httpContext.Response.StatusCode = (int)status;

            // If the response type is is null or Unit, then don't do anything since we have nothing to serialize
            if ((handlerResult.Response ?? Unit.Result) == Unit.Result)
                return;

            // Create a context so that our formatter knows how/where to serialize the response
            var formatterContext = httpContext.OutputFormatterContext(handlerResult);

            // Use our writer to write a reponse body
            IOutputFormatter outputFormatter = _outputFormatters.GetBestFormatter(formatterContext);
            await outputFormatter.WriteAsync(formatterContext);
        }
    }
}
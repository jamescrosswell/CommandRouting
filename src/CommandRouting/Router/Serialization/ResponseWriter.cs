using System;
using System.IO;
using System.Net;
using System.Text;
using CommandRouting.Handlers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.MemoryPool;

namespace CommandRouting.Router.Serialization
{
    internal class ResponseWriter
    {
        private readonly IOutputFormatter _outputFormatter;
        private readonly HttpContext _httpContext;
        private readonly Func<Stream, Encoding, TextWriter> _responseWriterFactory;

        public ResponseWriter(HttpContext httpContext, IOutputFormatter outputFormatter)
        {
            _httpContext = httpContext;
            _outputFormatter = outputFormatter;

            IArraySegmentPool<byte> byteSegmentPool = new DefaultArraySegmentPool<byte>();
            IArraySegmentPool<char> charSegmentPool = new DefaultArraySegmentPool<char>();
            IHttpResponseStreamWriterFactory writerFactory = new MemoryPoolHttpResponseStreamWriterFactory(byteSegmentPool, charSegmentPool);
            _responseWriterFactory = writerFactory.CreateWriter;
        }

        /// <summary>
        /// Creates an appropriate HttpResponse from our command handler result
        /// </summary>
        /// <param name="handlerResult">The command handler result that we need to create ah HttpResponse from</param>
        public void SerializeResponse(IHandlerResult handlerResult)
        {
            // Set the status code on the response
            var httpResponseResult = handlerResult as IHttpResponseResult;
            var status = httpResponseResult?.Status ?? HttpStatusCode.OK;
            _httpContext.Response.StatusCode = (int)status;

            // If the response type is is null or Unit, then don't do anything since we have nothing to serialize
            if ((handlerResult.Response ?? Unit.Result) == Unit.Result)
                return;

            // Create a context so that our formatter knows where to serialize the response
            var formatterContext = new OutputFormatterWriteContext(
                _httpContext,
                _responseWriterFactory,
                handlerResult.ResponseType,
                handlerResult.Response);

            // Use our writer to write a reponse body
            _outputFormatter.WriteAsync(formatterContext).Wait();
        }
    }
}
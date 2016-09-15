using System;
using System.Collections.Generic;
using System.Linq;
using CommandRouting.Handlers;
using CommandRouting.Router.Serialization;
using Microsoft.AspNetCore.Http;

namespace CommandRouting.Router
{
    public class CommandPipeline<TRequest>
    {
        private readonly IList<IRequestHandler<TRequest>> _commandHandlers;
        private readonly HttpVerb _verb;

        internal CommandPipeline(
            HttpVerb verb, 
            params IRequestHandler<TRequest>[] requestHandlers
            )
        {
            _commandHandlers = requestHandlers.ToList();
            _verb = verb;
        }

        internal void AddHandler(IRequestHandler<TRequest> handler)
        {
            _commandHandlers.Add(handler);
        }

        public bool CanHandleVerb(HttpContext httpContext)
        {
            return string.Equals(
                httpContext.Request.Method, $"{_verb}",
                StringComparison.OrdinalIgnoreCase
                );
        }

        public RequestDelegate GetRequestHandler(TRequest requestModel, IResponseWriter responseWriter)
        {
            // Otherwise create a simple anonymous delegate to dispatch the request 
            // through our command pipeline and serialize the result to the http response
            return context =>
            {
                var result = Dispatch(requestModel);
                return responseWriter.SerializeResponseAsync(result, context);
            };            
        }

        public IHandlerResult Dispatch(TRequest requestModel)
        {            
            // Run the command through our command pipeline until it gets handled
            foreach (var handler in _commandHandlers)
            {
                IHandlerResult handlerResult = handler.Dispatch(requestModel);
                if (handlerResult.IsHandled)
                    return handlerResult;
            }

            // Otherwise our pipeline can't handle the request
            return new NotHandled();
        }
    }
}

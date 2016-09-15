using System;
using System.Threading.Tasks;
using CommandRouting.Router.Serialization;
using Microsoft.AspNetCore.Routing;

namespace CommandRouting.Router
{
    public class CommandRoute<TRequest> : IRouter
    {
        private readonly CommandPipeline<TRequest> _pipeline;
        private readonly IRequestModelActivator _modelActivator;
        private readonly IResponseWriter _responseWriter;

        public CommandRoute(
            CommandPipeline<TRequest> pipeline, 
            IRequestModelActivator modelActivator, 
            IResponseWriter responseWriter
            )
        {
            if (pipeline == null) throw new ArgumentNullException(nameof(pipeline));
            if (modelActivator == null) throw new ArgumentNullException(nameof(modelActivator));
            if (responseWriter == null) throw new ArgumentNullException(nameof(responseWriter));
            _pipeline = pipeline;
            _modelActivator = modelActivator;
            _responseWriter = responseWriter;
        }

        public Task RouteAsync(RouteContext context)
        {
            // Build a request model from the request... note that we have to make special Unit type since it's a singleton
            object requestModel;
            if (typeof(TRequest) == typeof(Unit))
                requestModel = Unit.Result;
            else
                requestModel = _modelActivator.CreateRequestModelAsync<TRequest>(context.HttpContext, context.RouteData).Result;

            // Get a request handler from our command pipeline
            context.Handler = _pipeline.CanHandleVerb(context.HttpContext)
                ? _pipeline.GetRequestHandler((TRequest)requestModel, _responseWriter)
                : null;

            return Task.FromResult(0);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
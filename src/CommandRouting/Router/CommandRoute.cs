using System.Threading.Tasks;
using CommandRouting.Handlers;
using CommandRouting.Helpers;
using CommandRouting.Router.Serialization;
using Microsoft.AspNet.Routing;

namespace CommandRouting.Router
{
    public class CommandRoute<TRequest> : IRouter
    {
        private readonly IRequestModelActivator _modelActivator;
        private readonly CommandPipeline<TRequest> _pipeline;
        private readonly IResponseWriter _responseWriter;

        public CommandRoute(IRequestModelActivator modelActivator, CommandPipeline<TRequest> pipeline, IResponseWriter responseWriter)
        {
            Ensure.NotNull(modelActivator, nameof(modelActivator));
            Ensure.NotNull(pipeline, nameof(pipeline));
            Ensure.NotNull(responseWriter, nameof(responseWriter));

            _modelActivator = modelActivator;
            _pipeline = pipeline;
            _responseWriter = responseWriter;
        }

        public async Task RouteAsync(RouteContext context)
        {
            // Build a request model from the request... note that we have to make special Unit type since it's a singleton
            object requestModel;
            if (typeof(TRequest) == typeof(Unit))
               requestModel = Unit.Result;
            else
                requestModel = await _modelActivator.CreateRequestModelAsync<TRequest>(context);

            // Run the request through our command pipeline
            IHandlerResult pipelineResult = _pipeline.Dispatch(context.HttpContext, (TRequest)requestModel);

            // If the request was handled by our pipeline then write the response out
            if (pipelineResult.IsHandled)
            {
                // Serialize the response model                 
                await _responseWriter.SerializeResponseAsync(pipelineResult, context.HttpContext);

                // Let OWIN know our middleware handled the request
                context.IsHandled = true;
            }
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
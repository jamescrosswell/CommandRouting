using System;
using System.Threading.Tasks;
using CommandRouting.Handlers;
using CommandRouting.Router.Serialization;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Routing;

namespace CommandRouting.Router
{
    public class CommandRoute<TRequest> : IRouter
    {
        private readonly IRequestModelActivator _modelActivator;
        private readonly CommandPipeline<TRequest> _pipeline;

        public CommandRoute(IRequestModelActivator modelActivator, CommandPipeline<TRequest> pipeline)
        {
            if (modelActivator == null)
                throw new ArgumentNullException(nameof(modelActivator));
            if (pipeline == null)
                throw new ArgumentNullException(nameof(pipeline));

            _modelActivator = modelActivator;
            _pipeline = pipeline;
        }

        public async Task RouteAsync(RouteContext context)
        {                        
            // Build a request model from the request 
            TRequest requestModel = await _modelActivator.CreateRequestModelAsync<TRequest>(context);

            // Run the request through our command pipeline
            IHandlerResult pipelineResult = _pipeline.Dispatch(context.HttpContext, requestModel);

            // If the request was handled by our pipeline then write the response out
            if (pipelineResult.IsHandled)
            {
                // Serialize the response model                 
                IOutputFormatter outputFormatter = new JsonOutputFormatter();
                ResponseWriter responseWriter = new ResponseWriter(context.HttpContext, outputFormatter);
                await responseWriter.SerializeResponseAsync(pipelineResult);

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
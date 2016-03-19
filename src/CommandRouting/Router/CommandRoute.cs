using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandRouting.Handlers;
using CommandRouting.Router.Serialization;
using CommandRouting.Router.ValueParsers;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Routing;

namespace CommandRouting.Router
{
    public class CommandRoute<TRequest> : IRouter
    {
        private readonly CommandPipeline<TRequest> _pipeline;
        public CommandRoute(CommandPipeline<TRequest> pipeline)
        {
            _pipeline = pipeline;
        }

        public async Task RouteAsync(RouteContext context)
        {                        
            // Build a request model from the request 
            IInputFormatter inputFormatter = new JsonInputFormatter();
            IEnumerable<IValueParser> valueParsers = new List<IValueParser> { new RouteValueParser(context.RouteData) };
            RequestModelActivator modelActivator = new RequestModelActivator(
                context.HttpContext, 
                inputFormatter, 
                valueParsers
                );
            TRequest requestModel = await modelActivator.CreateRequestModelAsync<TRequest>();

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
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
        private readonly IList<ICommandHandler<TRequest>> _pipeline;
        public CommandRoute(params ICommandHandler<TRequest>[] pipeline)
        {
            _pipeline = pipeline.ToList();
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

            // Run the command through our command pipeline until it gets handled
            foreach (var handler in _pipeline)
            {
                IHandlerResult handlerResult = handler.Dispatch(requestModel);
                if (handlerResult.IsHandled)
                {
                    // Serialize the response model 
                    IOutputFormatter outputFormatter = new JsonOutputFormatter();
                    ResponseWriter responseWriter = new ResponseWriter(context.HttpContext, outputFormatter);
                    responseWriter.SerializeResponse(handlerResult);

                    // Let OWIN know our middleware handled the request
                    context.IsHandled = true;
                    break;
                }
            }
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CommandRouting.Handlers;
using Microsoft.AspNet.Http;

namespace CommandRouting.Router
{
    public class CommandPipeline<TRequest>
    {
        private readonly IList<ICommandHandler<TRequest>> _commandHandlers;
        private readonly HttpVerb _verb;

        internal CommandPipeline(HttpVerb verb, params ICommandHandler<TRequest>[] commandHandlers)
        {
            _commandHandlers = commandHandlers.ToList();
            _verb = verb;
        }

        internal void AddHandler(ICommandHandler<TRequest> handler)
        {
            _commandHandlers.Add(handler);
        }

        internal IHandlerResult Dispatch(HttpContext context, TRequest requestModel)
        {
            // Check to see if this pipeline handles the request verb
            bool pipelineHandlesVerb = string.Equals(
                context.Request.Method, $"{_verb}",
                StringComparison.OrdinalIgnoreCase
                );
            
            // Run the command through our command pipeline until it gets handled
            if (pipelineHandlesVerb)
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

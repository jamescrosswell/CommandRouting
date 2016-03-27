using System;
using CommandRouting.Router;

namespace CommandRouting.Config
{
    public class RoutePipelineBuilder
    {
        internal ICommandRouteBuilder CommandRouteBuilder { get; }
        internal HttpVerb Verb { get; }
        internal string RouteTemplate { get; }

        internal RoutePipelineBuilder(ICommandRouteBuilder commandRouteBuilder, HttpVerb verb, string routeTemplate)
        {
            CommandRouteBuilder = commandRouteBuilder;
            Verb = verb;
            RouteTemplate = routeTemplate;
        }
    }
}
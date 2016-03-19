using CommandRouting.Router;

namespace CommandRouting.Configure
{
    public class RoutePipelineBuilder
    {
        internal CommandRouteBuilder CommandRouteBuilder;
        internal HttpVerb Verb;
        internal string RouteTemplate;

        internal RoutePipelineBuilder(CommandRouteBuilder commandRouteBuilder, HttpVerb verb, string routeTemplate)
        {
            CommandRouteBuilder = commandRouteBuilder;
            Verb = verb;
            RouteTemplate = routeTemplate;
        }
    }
}
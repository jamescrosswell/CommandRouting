namespace CommandRouting.Configure
{
    public class RoutePipelineBuilder
    {
        internal CommandRouteBuilder CommandRouteBuilder;
        internal string RouteTemplate;

        public RoutePipelineBuilder(CommandRouteBuilder commandRouteBuilder, string routeTemplate)
        {
            CommandRouteBuilder = commandRouteBuilder;
            RouteTemplate = routeTemplate;
        }
    }
}
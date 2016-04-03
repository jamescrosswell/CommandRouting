namespace CommandRouting.Config
{
    /// <summary>
    /// Extensions that give us our fluent syntax for mapping to route sets:
    /// <example> 
    /// <code>
    ///     CommandPipelineBuilder routeBuilder = new CommandPipelineBuilder(IServiceProvider ServiceProvider);
    ///     routeBuilder
    ///         .Map("account")
    ///         .To&lt;AccountCommands&gt;();
    /// </code>
    /// </example>
    /// </summary>
    public static class FluentCommandSets
    {

        public static RouteSetPrefixBuilder Map(this ICommandRouteBuilder builder, string prefix = "")
        {
            return new RouteSetPrefixBuilder(builder, prefix);
        }

        public static void To<TCommandSet>(this RouteSetPrefixBuilder builder)
            where TCommandSet : IRouteSet
        {
            MapCommands<TCommandSet>(builder.CommandRouteBuilder, builder.Prefix);
        }

        public static void MapCommands<TCommandSet>(this ICommandRouteBuilder builder, string prefix = "")
            where TCommandSet : IRouteSet
        {
            // Create a CommandSetRouteBuilderDecorator to register all the routes defined in the route set.
            // This makes sure the routes get prefixed if necessary
            var commandSetRouteBuilder = new RouteSetRouteBuilderDecorator(builder, prefix);

            // Activate the CommandSet
            IRouteSet commandSet = commandSetRouteBuilder.ActivateCommandSet<TCommandSet>();

            // Have the route set register it's routes using our decorated route builder
            commandSet.Configure(commandSetRouteBuilder);
        }
    }
}
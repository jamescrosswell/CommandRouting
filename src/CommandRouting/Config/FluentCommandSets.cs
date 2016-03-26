namespace CommandRouting.Config
{
    /// <summary>
    /// Extensions that give us our fluent syntax for mapping to command sets:
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

        public static CommandSetPrefixBuilder Map(this ICommandRouteBuilder builder, string prefix = "")
        {
            return new CommandSetPrefixBuilder(builder, prefix);
        }

        public static void To<TCommandSet>(this CommandSetPrefixBuilder builder)
            where TCommandSet : ICommandSet
        {
            MapCommands<TCommandSet>(builder.CommandRouteBuilder, builder.Prefix);
        }

        public static void MapCommands<TCommandSet>(this ICommandRouteBuilder builder, string prefix = "")
            where TCommandSet : ICommandSet
        {
            // Create a CommandSetRouteBuilderDecorator to register all the routes defined in the command set.
            // This makes sure the routes get prefixed if necessary
            var commandSetRouteBuilder = new CommandSetRouteBuilderDecorator(builder, prefix);

            // Activate the CommandSet
            ICommandSet commandSet = commandSetRouteBuilder.ActivateCommandSet<TCommandSet>();

            // Have the command set register it's routes using our decorated route builder
            commandSet.Configure(commandSetRouteBuilder);
        }
    }
}
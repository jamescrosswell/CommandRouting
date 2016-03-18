namespace CommandRouting.Configure
{
    /// <summary>
    /// Extensions that give us our fluent syntax for building command routes:
    /// <example> 
    /// <code>
    ///     CommandPipelineBuilder pipelines = new CommandPipelineBuilder(IServiceProvider ServiceProvider);
    ///     pipelines
    ///         .Pipe("hello/{name:alpha}")
    ///         .As&lt;SayHelloRequest&gt;()
    ///         .To&lt;IgnoreBob, SayHello&gt;();
    /// </code>
    /// </example>
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        public static RoutePipelineBuilder Route(this CommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, routeTemplate);
        }

        public static CommandPipelineBuilder<TRequest> As<TRequest>(this RoutePipelineBuilder builder)
        {
            return new CommandPipelineBuilder<TRequest>(builder);
        }
    }
}
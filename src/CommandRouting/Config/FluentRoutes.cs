using CommandRouting.Router;

namespace CommandRouting.Config
{
    /// <summary>
    /// Extensions that give us our fluent syntax for building command routes:
    /// <example> 
    /// <code>
    ///     CommandPipelineBuilder pipelines = new CommandPipelineBuilder(IServiceProvider ServiceProvider);
    ///     pipelines
    ///         .Get("hello/{name:alpha}")
    ///         .As&lt;SayHelloRequest&gt;()
    ///         .RoutesTo&lt;IgnoreBob, SayHello&gt;();
    /// </code>
    /// </example>
    /// </summary>
    public static class FluentRoutes
    {
        #region Routes

        public static RoutePipelineBuilder Delete(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Delete, routeTemplate);
        }

        public static RoutePipelineBuilder Get(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Get, routeTemplate);
        }

        public static RoutePipelineBuilder Head(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Head, routeTemplate);
        }

        public static RoutePipelineBuilder Options(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Options, routeTemplate);
        }

        public static RoutePipelineBuilder Patch(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Patch, routeTemplate);
        }

        public static RoutePipelineBuilder Post(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Post, routeTemplate);
        }

        public static RoutePipelineBuilder Put(this ICommandRouteBuilder builder, string routeTemplate)
        {
            return new RoutePipelineBuilder(builder, HttpVerb.Put, routeTemplate);
        }

        #endregion

        #region Commands

        public static CommandPipelineBuilder<TRequest> As<TRequest>(this RoutePipelineBuilder builder)
        {
            return new CommandPipelineBuilder<TRequest>(builder);
        }

        #endregion
    }
}
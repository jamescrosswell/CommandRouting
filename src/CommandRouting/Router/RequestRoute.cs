using System;
using CommandRouting.Helpers;

namespace CommandRouting.Router
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public abstract class RouteRequestAttribute : Attribute
    {
        internal abstract HttpVerb HttpVerb { get; }
        internal Type[] CommandHandlers { get; }
        internal string RouteTemplate { get; }

        protected RouteRequestAttribute(string routeTemplate, Type[] commandHandlers)
        {
            Ensure.NotNull(routeTemplate, nameof(routeTemplate));
            Ensure.NotNull(commandHandlers, nameof(commandHandlers));
            RouteTemplate = routeTemplate;
            CommandHandlers = commandHandlers;
        }
    }

    public sealed class GetRouteRequestAttribute : RouteRequestAttribute
    {
        internal override HttpVerb HttpVerb => HttpVerb.Get;

        public GetRouteRequestAttribute(string routeTemplate, params Type[] commandHandlers)
             : base(routeTemplate, commandHandlers)
        {
            if (commandHandlers == null) throw new ArgumentNullException(nameof(commandHandlers));
        }
    }

    public sealed class PostRouteRequestAttribute : RouteRequestAttribute
    {
        internal override HttpVerb HttpVerb => HttpVerb.Post;

        public PostRouteRequestAttribute(string routeTemplate, params Type[] commandHandlers)
             : base(routeTemplate, commandHandlers)
        {
        }
    }
}

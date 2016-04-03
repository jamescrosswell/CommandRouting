using System;
using CommandRouting.Helpers;
using CommandRouting.Router;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRouting.Config
{
    /// <summary>
    /// This decorator ensures all routes registered with this route builder have a prefix (if one is defined).
    /// </summary>
    internal class RouteSetRouteBuilderDecorator: ICommandRouteBuilder
    {
        private readonly ICommandRouteBuilder _parentRouteBuilder;

        internal readonly string NakedPrefix;

        public RouteSetRouteBuilderDecorator(ICommandRouteBuilder parentRouteBuilder, string prefix = "")
        {
            // Store the parent
            _parentRouteBuilder = parentRouteBuilder;

            // Strip any leading or trailing slashes from the prefix and store it in our naked prefix
            NakedPrefix = (prefix ?? "").Strip("/");
        }

        internal IRouteSet ActivateCommandSet<TCommandSet>()
        {
            // Have our service provider create an instance of the route set... in case it has any
            // dependencies that also require activation
            return (IRouteSet)ActivatorUtilities.CreateInstance(ServiceProvider, typeof(TCommandSet));
        }

        // Delegate to the parent - we're just a decorator
        public IServiceProvider ServiceProvider => _parentRouteBuilder.ServiceProvider;

        /// <summary>
        /// Adds a route for the <paramref name="routeTemplate"/>, including any prefix that 
        /// might have been configured for the route set
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="verb"></param>
        /// <param name="routeTemplate"></param>
        /// <param name="commandHandlerTypes"></param>
        void ICommandRouteBuilder.AddRoute<TRequest>(HttpVerb verb, string routeTemplate, Type[] commandHandlerTypes)
        {
            // Add a prefix to the route template, if necessary
            routeTemplate = PrefixRouteTemplate(routeTemplate);

            // Now have the parent route builder do the grunt work
            _parentRouteBuilder.AddRoute<TRequest>(verb, routeTemplate, commandHandlerTypes);
        }

        /// <summary>
        /// If our route template has a prefix
        /// </summary>
        /// <param name="routeTemplate"></param>
        /// <returns></returns>
        internal string PrefixRouteTemplate(string routeTemplate)
        {
            routeTemplate = (routeTemplate ?? "").Strip("/");
            return (NakedPrefix.IsBlank())
                ? routeTemplate
                : $"{NakedPrefix}/{routeTemplate}";
        }

    }
}

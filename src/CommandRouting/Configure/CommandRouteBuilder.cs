using System;
using System.Collections.Generic;
using CommandRouting.Handlers;
using CommandRouting.Router;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRouting.Configure
{
    /// <summary>
    /// Provide extensions to register command handlers automatically with the
    /// dependency injection container
    /// </summary>
    public class CommandRouteBuilder : IRouteBuilder
    {
        public IRouter DefaultHandler { get; set; }
        public IServiceProvider ServiceProvider { get; }
        public IList<IRouter> Routes { get; }

        public CommandRouteBuilder(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            ServiceProvider = serviceProvider;
            Routes = new List<IRouter>();
        }

        internal void AddRoute<TRequest>(HttpVerb verb, string routeTemplate, Type[] commandHandlerTypes)
        {
            // Instanciate concrete instances for each handler in the command pipeline
            CommandPipeline<TRequest> pipeline = new CommandPipeline<TRequest>(verb);
            foreach (Type handlerType in commandHandlerTypes)
            {
                var handler = (ICommandHandler<TRequest>)ActivatorUtilities.CreateInstance(ServiceProvider, handlerType);
                pipeline.AddHandler(handler);
            }

            // Register a route for the command pipeline
            var constraintsResolver = ServiceProvider.GetService<IInlineConstraintResolver>();
            Routes.Add(new TemplateRoute(
                new CommandRoute<TRequest>(pipeline),
                routeTemplate,
                constraintsResolver
                ));
        }

        public IRouter Build()
        {
            var routeCollection = new RouteCollection();
            foreach (var router in Routes)
                routeCollection.Add(router);
            return routeCollection;
        }
    }
}

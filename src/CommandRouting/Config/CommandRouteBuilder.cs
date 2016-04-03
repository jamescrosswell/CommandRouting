using System;
using System.Collections.Generic;
using CommandRouting.Handlers;
using CommandRouting.Router;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRouting.Config
{
    /// <summary>
    /// Provide extensions to register request handlers automatically with the
    /// dependency injection container
    /// </summary>
    public class CommandRouteBuilder : ICommandRouteBuilder, IRouteBuilder
    {
        public IRouter DefaultHandler { get; set; }
        public IServiceProvider ServiceProvider { get; }
        public IList<IRouter> Routes { get; }

        private readonly IInlineConstraintResolver _constraintResolver;

        public CommandRouteBuilder(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            ServiceProvider = serviceProvider;
            Routes = new List<IRouter>();

            // Additional dependencies that are required for command routing - this is kind of a service
            // locator antipattern but that's an almost unavoidable side effect of implementing IRouteBuilder
            // and kind of the price we pay for making the setup syntax nice and simple in the Startup class.
            _constraintResolver = serviceProvider.GetService<IInlineConstraintResolver>();
            if (_constraintResolver == null)
                throw new InvalidOperationException($"Unable to find service: {nameof(IInlineConstraintResolver)}");
        }

        private IRequestHandler<TRequest> ActivateCommandHandler<TRequest>(Type handlerType)
        {
            // The ActivatorUtilities let us use the service provider to create instances of a class
            // that hasn't been registered with the service provider... the reason for doing this is
            // because the service provider can automatically resolve any of the dependencies that the
            // command handler specifies.
            return (IRequestHandler<TRequest>)ActivatorUtilities.CreateInstance(ServiceProvider, handlerType);
        }

        public void AddRoute<TRequest>(HttpVerb verb, string routeTemplate, Type[] commandHandlerTypes)
        {
            // Instanciate concrete instances for each handler in the command pipeline
            CommandPipeline<TRequest> pipeline = new CommandPipeline<TRequest>(verb);
            foreach (Type handlerType in commandHandlerTypes)
            {
                var handler = ActivateCommandHandler<TRequest>(handlerType);
                pipeline.AddHandler(handler);
            }

            // Register a route for the command pipeline
            var commandRoute = ActivatorUtilities.CreateInstance<CommandRoute<TRequest>>(ServiceProvider, pipeline);
            Routes.Add(new TemplateRoute(
                commandRoute,
                routeTemplate,
                _constraintResolver
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

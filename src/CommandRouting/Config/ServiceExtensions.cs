using System;
using CommandRouting.Router;
using CommandRouting.Router.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRouting.Config
{
    public static class ServiceExtensions
    {
        public static void AddCommandRouting(this IServiceCollection services, Action<CommandRoutingOptions> initConfig = null)
        {
            // Setup a configuration object as a singleton and let the user override any
            // of the configuration properties via an optional action method
            CommandRoutingOptions options = new CommandRoutingOptions();
            initConfig?.Invoke(options);
            services.AddInstance(options.InputFormatters);
            services.AddInstance(options.OutputFormatters);
            services.AddInstance(options.ValueParsers);

            // Register other dependencies that we want to inject
            services.AddSingleton<IRequestModelActivator, RequestModelActivator>();
            services.AddSingleton<IRequestReader, RequestReader>();
            services.AddSingleton<IResponseWriter, ResponseWriter>();
        }

        /// <summary>
        /// Simple extension that allows using generics (and takes care of casting) rather than 
        /// passing type parameters explicitly.
        /// </summary>
        /// <typeparam name="T">The type of the service dependecy that we want to resolve</typeparam>
        /// <param name="serviceProvider">The service container used to resolve the dependency</param>
        /// <returns>An instance of the service</returns>
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof (T));
        }
    }
}

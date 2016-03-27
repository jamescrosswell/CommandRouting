using System;
using CommandRouting.Router;

namespace CommandRouting.Config
{
    public interface ICommandRouteBuilder
    {
        IServiceProvider ServiceProvider { get; }
        void AddRoute<TRequest>(HttpVerb verb, string routeTemplate, Type[] commandHandlerTypes);
    }
}
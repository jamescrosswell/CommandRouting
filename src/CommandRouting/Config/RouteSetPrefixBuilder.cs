using System;
using CommandRouting.Handlers;
using CommandRouting.Router;

namespace CommandRouting.Config
{
    public class RouteSetPrefixBuilder
    {
        internal ICommandRouteBuilder CommandRouteBuilder { get; }
        internal string Prefix { get; }

        internal RouteSetPrefixBuilder(ICommandRouteBuilder commandRouteBuilder, string prefix)
        {
            CommandRouteBuilder = commandRouteBuilder;
            Prefix = prefix;
        }
    }
}
using System;
using CommandRouting.Handlers;
using CommandRouting.Router;

namespace CommandRouting.Config
{
    public class CommandSetPrefixBuilder
    {
        internal ICommandRouteBuilder CommandRouteBuilder { get; }
        internal string Prefix { get; }

        internal CommandSetPrefixBuilder(ICommandRouteBuilder commandRouteBuilder, string prefix)
        {
            CommandRouteBuilder = commandRouteBuilder;
            Prefix = prefix;
        }
    }
}
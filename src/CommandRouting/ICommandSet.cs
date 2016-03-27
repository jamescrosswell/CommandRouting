using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandRouting.Config;

namespace CommandRouting
{
    /// <summary>
    /// <para>
    /// Command sets provide a way of composing command routes. Rather than registering 
    /// all of your routes in one place, you can instead group routes into command sets 
    /// that you register. Each of these can configure 0..n command routes and can also 
    /// then delegate the registration of other routes to further command sets, resulting 
    /// in a tree of routes that get's composed recursively.
    /// </para>
    /// <para>
    /// In this way, the registration of command routes can be delegated from the 
    /// startup module to command sets in a hierarchical manner. 
    /// </para>
    /// <para>
    /// Additionally, command sets can optionally have a prefix that is assigned to them
    /// when you register these - which is an easy way to ensure consistent routing to
    /// related pipelines of command handlers.
    /// </para>
    /// </summary>
    public interface ICommandSet
    {
        void Configure(ICommandRouteBuilder builder);
    }
}

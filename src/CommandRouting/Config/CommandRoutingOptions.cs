using System.Collections.Generic;
using CommandRouting.Router.ValueParsers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRouting.Config
{
    /// <summary>
    /// Stores various configuration settings for resolving and dispatching
    /// command routes. 
    /// </summary>
    public class CommandRoutingOptions
    {
        internal CommandRoutingOptions()
        {
            // Setup default options
            //InputFormatters = new List<IInputFormatter> { new JsonInputFormatter() };
            //OutputFormatters = new List<IOutputFormatter> { new JsonOutputFormatter() };
            ValueParsers = new List<IValueParser> { new RouteValueParser() };
        }

        //public IEnumerable<IInputFormatter> InputFormatters { get; }
        //public IEnumerable<IOutputFormatter> OutputFormatters { get; }
        public IEnumerable<IValueParser> ValueParsers { get; }
    }
}

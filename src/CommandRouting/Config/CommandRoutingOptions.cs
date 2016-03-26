using System.Collections.Generic;
using CommandRouting.Router.ValueParsers;
using Microsoft.AspNet.Mvc.Formatters;

namespace CommandRouting.Config
{
    /// <summary>
    /// Stores various configuration settings for resolving and dispatching
    /// command routes. 
    /// </summary>
    public class CommandRoutingOptions
    {
        public CommandRoutingOptions()
        {
            // Setup default options
            InputFormatters = new List<IInputFormatter> { new JsonInputFormatter() };
            OutputFormatters = new List<IOutputFormatter> { new JsonOutputFormatter() };
            ValueParsers = new List<IValueParser> { new RouteValueParser() };
        }

        public IEnumerable<IInputFormatter> InputFormatters { get; }
        public IEnumerable<IOutputFormatter> OutputFormatters { get; }
        public IEnumerable<IValueParser> ValueParsers { get; }
    }
}

using System.Collections.Generic;
using CommandRouting.Router.ValueParsers;
using Microsoft.AspNet.Mvc.Formatters;

namespace CommandRouting.Config
{
    #region Interface segregation

    public interface ICommandRoutingOptions : 
        IInputFormatterOptions,
        IOutputFormatterOptions,
        IValueParserOptions
    {
        
    }

    public interface IInputFormatterOptions
    {
        IEnumerable<IInputFormatter> InputFormatters { get; }
    }

    public interface IOutputFormatterOptions
    {
        IEnumerable<IOutputFormatter> OutputFormatters { get; }
    }

    public interface IValueParserOptions
    {
        IEnumerable<IValueParser> ValueParsers { get; }
    }

    #endregion

    /// <summary>
    /// Stores various configuration settings for resolving and dispatching
    /// command routes. 
    /// </summary>
    public class CommandRoutingOptions : ICommandRoutingOptions
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

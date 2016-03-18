using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Routing;

namespace CommandRouting.Router.ValueParsers
{
    public class RouteValueParser : IValueParser
    {
        private readonly RouteData _routeData;

        public RouteValueParser(RouteData routeData)
        {
            _routeData = routeData;
        }

        /// <summary>
        /// Merges values into our request model from the route data. This is *very* simple compared to the
        /// MCV 6 model binding. For a start, we ignore other value sources such as query strings and form data. 
        /// Secondly, we only bind simple value types that can be converted directly from a string.
        /// </summary>
        /// <param name="requestModel"></param>
        public void ParseValues(object requestModel)
        {
            // For each route value, check to see if we can assign the value to our request model
            foreach (var kvp in _routeData.Values)
            {
                requestModel.TryParseDeepPropertyValue(kvp.Key, kvp.Value as string);
            }
        }
    }
}
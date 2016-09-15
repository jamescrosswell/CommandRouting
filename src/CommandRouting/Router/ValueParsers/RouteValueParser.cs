using Microsoft.AspNetCore.Routing;

namespace CommandRouting.Router.ValueParsers
{
    public class RouteValueParser : IValueParser
    {
        /// <summary>
        /// Merges values into our request model from the route data. This is *very* simple compared to the
        /// MCV 6 model binding. For a start, we ignore other value sources such as query strings and form data. 
        /// Secondly, we only bind simple value types that can be converted directly from a string.
        /// </summary>
        /// <param name="routeData">The route data to be parsed</param>
        /// <param name="requestModel">The request model where we want properties from the route data to be set</param>
        public void ParseValues(RouteData routeData, object requestModel)
        {
            // For each route value, check to see if we can assign the value to our request model
            foreach (var kvp in routeData.Values)
            {
                requestModel.TryParseDeepPropertyValue(kvp.Key, kvp.Value as string);
            }
        }
    }
}
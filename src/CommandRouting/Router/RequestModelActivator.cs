using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Routing;

namespace CommandRouting.Router
{
    /// <summary>
    /// Used to create an instance of a command request model from the body of an an HTTP Request. 
    /// </summary>
    public class RequestModelActivator
    {
        private readonly HttpContext _httpContext;
        private readonly RouteData _routeData;

        // List of input formatters (currently we only support json)
        IInputFormatter[] InputFormatters => new IInputFormatter[] { new JsonInputFormatter() };

        public RequestModelActivator(HttpContext httpContext, RouteData routeData)
        {
            // Make sure the httpContext is not null
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            _httpContext = httpContext;
            _routeData = routeData ?? new RouteData();
        }

        public object CreateRequestModel(Type requestType)
        {
            // We'll try to create a basic model from the message body or, failing that, just create a 
            // default instance of the model
            dynamic model = DeserializeMessageBody(requestType) ?? Activator.CreateInstance(requestType);

            // Merge in any values from the route data
            MergeRouteValues(model);

            // Return the result
            return model;
        }

        /// <summary>
        /// Merges values into our request model from the route data. This is *very* simple compared to the
        /// MCV 6 model binding. For a start, we ignore other value sources such as query strings and form data. 
        /// Secondly, we only bind simple value types that can be converted directly from a string.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="requestType"></param>
        private void MergeRouteValues(object requestModel)
        {
            // For each route value, check to see if we can assign the value to our request model
            foreach (var kvp in _routeData.Values)
            {
                requestModel.TrySetDeepPropertyStringValue(kvp.Key, kvp.Value as string);
            }
        }

        /// <summary>
        /// Read the model from the message body using a JsonInputFormatter
        /// </summary>
        /// <param name="requestType">The type of the request model that we will try to activate</param>
        /// <returns>An instance of requestType</returns>
        private dynamic DeserializeMessageBody(Type requestType)
        {
            // First create a context so that our formatter knows how to 
            // deserialize the model
            var context = new InputFormatterContext(
                _httpContext,
                modelName: string.Empty,
                modelState: new ModelStateDictionary(),
                metadata: requestType.MetaData());

            // Next create a formatter (we only support JSON at the moment)
            var formatter = new JsonInputFormatter();

            // Finally, have the formatter return a model from the http request body
            return formatter.ReadAsync(context).Result.Model;
        }
    }
}


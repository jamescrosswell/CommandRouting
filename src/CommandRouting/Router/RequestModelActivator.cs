using System;
using System.Collections.Generic;
using CommandRouting.Router.Serialization;
using CommandRouting.Router.ValueParsers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;

namespace CommandRouting.Router
{
    /// <summary>
    /// Used to create an instance of a command request model from the body of an an HTTP Request. 
    /// </summary>
    public class RequestModelActivator
    {
        private readonly HttpContext _httpContext;
        private readonly IInputFormatter _inputFormatter;
        private readonly IEnumerable<IValueParser> _valueParsers;

        /// <summary>
        /// Creates a RequestModelActivator
        /// </summary>
        /// <param name="httpContext">An httpContext instance</param>
        /// <param name="inputFormatter">An input formatter to use when deserializing the message body</param>
        /// <param name="valueParsers">
        /// Zero or more value parsers that can set properties on the request model from sources other
        /// than the message body (for example from route data).
        /// </param>
        public RequestModelActivator(HttpContext httpContext, IInputFormatter inputFormatter, IEnumerable<IValueParser> valueParsers)
        {
            // Make sure the httpContext is not null
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            _httpContext = httpContext;
            _valueParsers = valueParsers;
            _inputFormatter = inputFormatter;
        }

        /// <summary>
        /// Create a request model by first deserializing the message body and then parsing properties from
        /// values (such as route data)
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of the request model that we want to create. 
        /// </typeparam>
        /// <returns>An instance of Type requestType</returns>
        public TRequest CreateRequestModel<TRequest>()
        {
            // Try to deserialize the message body to the appropriate request model or create a default instance
            Type requestType = typeof (TRequest);
            var requestReader = new RequestReader(_httpContext, _inputFormatter);
            dynamic model = requestReader.DeserializeRequest(requestType) ?? Activator.CreateInstance(requestType);

            // Merge in any values from the value parsers
            foreach (var parser in _valueParsers)
            {
                parser.ParseValues(model);
            }

            // Return the result
            return (TRequest)model;
        }

    }
}


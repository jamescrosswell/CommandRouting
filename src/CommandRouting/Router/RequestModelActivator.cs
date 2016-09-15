using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandRouting.Router.Serialization;
using CommandRouting.Router.ValueParsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CommandRouting.Router
{
    public interface IRequestModelActivator
    {
        /// <summary>
        /// Create a request model by first deserializing the message body and then parsing properties from
        /// values (such as route data)
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of the request model that we want to create. 
        /// </typeparam>
        /// <returns>An instance of Type requestType</returns>
        Task<TRequest> CreateRequestModelAsync<TRequest>(HttpContext httpContext, RouteData routeData);
    }

    /// <summary>
    /// Used to create an instance of a command request model from the body of an an HTTP Request. 
    /// </summary>
    public class RequestModelActivator : IRequestModelActivator
    {
        private readonly IRequestReader _requestReader;
        private readonly IEnumerable<IValueParser> _valueParsers;

        /// <summary>
        /// Creates a RequestModelActivator
        /// </summary>
        /// <param name="requestReader">
        /// Input formatters that can be used to deserialize the message
        /// </param>
        /// <param name="valueParsers">
        /// Value parsers that can be used to set request model properties from sources 
        /// other than the message body (for example from route data).
        /// </param>
        public RequestModelActivator(IRequestReader requestReader, IEnumerable<IValueParser> valueParsers)
        {
            if (requestReader == null)
                throw new ArgumentNullException(nameof(requestReader));
            if (valueParsers == null)
                throw new ArgumentNullException(nameof(valueParsers));

            _requestReader = requestReader;
            _valueParsers = valueParsers;
        }

        /// <inheritdoc />
        public async Task<TRequest> CreateRequestModelAsync<TRequest>(HttpContext httpContext, RouteData routeData)
        {
            // Validate method parameters
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (routeData == null) throw new ArgumentNullException(nameof(routeData));

            // Try to deserialize the message body to the appropriate request model or create a default instance
            var model = await _requestReader.DeserializeRequestAsync<TRequest>(httpContext);

            // Merge in any values from the value parsers
            foreach (var parser in _valueParsers)
                parser.ParseValues(routeData, model);

            // Return the result
            return model;
        }

    }
}


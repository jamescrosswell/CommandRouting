using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace CommandRouting.Router.Serialization
{
    internal class RequestReader
    {
        private readonly IInputFormatter _inputFormatter;
        private readonly HttpContext _httpContext;

        public RequestReader(HttpContext httpContext, IInputFormatter inputFormatter)
        {
            _httpContext = httpContext;
            _inputFormatter = inputFormatter;
        }

        /// <summary>
        /// Deserialize the request model from the message body
        /// </summary>
        /// <param name="requestType">The type of the request model that we will try to activate</param>
        /// <returns>An instance of requestType</returns>
        public async Task<object> DeserializeRequestAsync(Type requestType)
        {
            // First create a context so that our formatter knows how to deserialize the model
            var context = new InputFormatterContext(
                _httpContext,
                string.Empty,
                new ModelStateDictionary(),
                requestType.MetaData()
                );

            // Finally, have the formatter return a model from the http request body
            var inputFormatterResult = await _inputFormatter.ReadAsync(context);
            return inputFormatterResult.Model ?? Activator.CreateInstance(requestType);
        }

        public async Task<TRequest> DeserializeRequestAsync<TRequest>()
        {
            var model = await DeserializeRequestAsync(typeof(TRequest));
            return (TRequest)model;
        }
    }
}
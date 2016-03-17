using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace CommandRouting.Router.Deserializers
{
    internal class RequestDeserializer
    {
        private readonly IInputFormatter _inputFormatter;
        private readonly HttpContext _httpContext;

        public RequestDeserializer(HttpContext httpContext, IInputFormatter inputFormatter)
        {
            _httpContext = httpContext;
            _inputFormatter = inputFormatter;
        }

        /// <summary>
        /// Deserialize the request model from the message body
        /// </summary>
        /// <param name="requestType">The type of the request model that we will try to activate</param>
        /// <returns>An instance of requestType</returns>
        public dynamic DeserializeMessage(Type requestType)
        {
            // First create a context so that our formatter knows how to 
            // deserialize the model
            var context = new InputFormatterContext(
                _httpContext,
                modelName: string.Empty,
                modelState: new ModelStateDictionary(),
                metadata: requestType.MetaData());

            // Finally, have the formatter return a model from the http request body
            return _inputFormatter.ReadAsync(context).Result.Model;
        }
    }
}
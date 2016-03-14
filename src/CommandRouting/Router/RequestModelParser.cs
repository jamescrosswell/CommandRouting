using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace CommandRouting.Router
{
    /// <summary>
    /// Used to create an instance of a command request model from the body of an an HTTP Request. 
    /// </summary>
    public class RequestModelParser
    {
        private readonly HttpContext _httpContext;

        public RequestModelParser(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public object CreateRequestModel(Type requestType)
        {
            // Create a JsonInputFormatter to read the json model from the message body                   
            var formatter = new JsonInputFormatter();
            var provider = new EmptyModelMetadataProvider();
            var metadata = provider.GetMetadataForType(requestType);
            var context = new InputFormatterContext(
                _httpContext,
                modelName: string.Empty,
                modelState: new ModelStateDictionary(),
                metadata: metadata);
            return formatter.ReadAsync(context).Result.Model;
        }        
    }
}

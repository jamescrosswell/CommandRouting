using System;
using System.Threading.Tasks;
using CommandRouting.Helpers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;

namespace CommandRouting.Router.Serialization
{
    public interface IRequestReader
    {
        /// <summary>
        /// Deserialize the request model from the message body
        /// </summary>
        /// <typeparam name="TRequest">The type of the request model that we will try to activate</typeparam>
        /// <param name="httpContext">The http context of the request that we want to deserialize</param>
        /// <returns>An instance of requestType</returns>
        Task<TRequest> DeserializeRequestAsync<TRequest>(HttpContext httpContext);
    }

    public class RequestReader : IRequestReader
    {
        private readonly IInputFormatSelector _inputFormatSelector;

        public RequestReader(IInputFormatSelector inputFormatSelector)
        {

            if (inputFormatSelector == null)
                throw new ArgumentNullException(nameof(inputFormatSelector));
            _inputFormatSelector = inputFormatSelector;
        }

        /// <inheritdoc />
        public async Task<TRequest> DeserializeRequestAsync<TRequest>(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            // Work out what input format to use
            InputFormatterContext formatContext = httpContext.InputFormatterContext<TRequest>();
            IInputFormatter inputFormatter = _inputFormatSelector.GetFormatterForContext(formatContext);

            // Have the formatter dezerialize a model from the http request body
            var inputFormatterResult = await inputFormatter.ReadAsync(formatContext);
            return (TRequest)(inputFormatterResult.Model ?? Activator.CreateInstance<TRequest>());
        }
    }
}
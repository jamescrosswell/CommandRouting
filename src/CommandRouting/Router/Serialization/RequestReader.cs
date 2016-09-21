using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandRouting.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

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
        private readonly IOptions<MvcOptions> _options;

        public RequestReader(IOptions<MvcOptions> options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options;
                    }

        /// <inheritdoc />
        public async Task<TRequest> DeserializeRequestAsync<TRequest>(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            // Work out what input format to use
            var formatContext = httpContext.InputFormatterContext<TRequest>();
            var inputFormatter = _options.Value.InputFormatters.GetBestFormatter(formatContext);

            // Have the formatter dezerialize a model from the http request body
            var inputFormatterResult = await inputFormatter.ReadAsync(formatContext);
            return (TRequest)(inputFormatterResult.Model ?? Activator.CreateInstance<TRequest>());
        }
    }
}
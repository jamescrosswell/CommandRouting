using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;

namespace CommandRouting.Router.Serialization
{
    public static class InputFormatSelector
    {
        internal static IInputFormatter GetBestFormatter(this IEnumerable<IInputFormatter> inputFormatters, InputFormatterContext context)
        {
            if (inputFormatters == null) throw new ArgumentNullException(nameof(inputFormatters));

            // Convert to an array, to avoid multiple enumeration
            var formatters = inputFormatters as IInputFormatter[] ?? inputFormatters.ToArray();

            // Now get the best formatter (we'll use the first one if we can't find anything better)
            IInputFormatter bestFormatter = formatters.FirstOrDefault(x => x.CanRead(context));
            return bestFormatter ?? formatters.FirstOrDefault();
        }

        /// <summary>
        /// Creates a default input format context (no model state and no model name) for 
        /// a <typeparamref name="TRequest"/> model 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static InputFormatterContext InputFormatterContext<TRequest>(this HttpContext context)
        {
            Func<Stream, Encoding, TextReader> readerFactory = (stream, encoding) => new HttpRequestStreamReader(stream, encoding);
            return new InputFormatterContext(
                context,
                string.Empty,
                new ModelStateDictionary(),
                typeof(TRequest).MetaData(),
                readerFactory
                );
        }
    }
}

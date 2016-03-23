using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace CommandRouting.Helpers
{
    internal static class FormatterContextHelper
    {
        /// <summary>
        /// Creates a default input format context (no model state and no model name) for 
        /// a <typeparamref name="TRequest"/> model 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static InputFormatterContext InputFormatterContext<TRequest>(this HttpContext context)
        {
            return new InputFormatterContext(
                context,
                string.Empty,
                new ModelStateDictionary(),
                typeof(TRequest).MetaData()
                );
        }
    }
}

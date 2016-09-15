using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using CommandRouting.Handlers;
using CommandRouting.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;

namespace CommandRouting.Router.Serialization
{
    public static class OutputFormatSelector
    {
        internal static IOutputFormatter GetBestFormatter(
            this IEnumerable<IOutputFormatter> outputFormatters, OutputFormatterWriteContext context)
        {
            if (outputFormatters == null) throw new ArgumentNullException(nameof(outputFormatters));

            // Convert to an array, to avoid multiple enumeration
            var formatters = outputFormatters as IOutputFormatter[] ?? outputFormatters.ToArray();

            // Now get the best formatter (we'll use the first one if we can't find anything better)
            IOutputFormatter bestFormatter = formatters.FirstOrDefault(x => x.CanWriteResult(context));
            return bestFormatter ?? formatters.FirstOrDefault();
        }

        internal static OutputFormatterWriteContext OutputFormatterContext(this HttpContext context, IHandlerResult handlerResult)
        {
            var bytePool = ArrayPool<byte>.Shared;
            var charPool = ArrayPool<char>.Shared;
            IHttpResponseStreamWriterFactory writerFactory = new MemoryPoolHttpResponseStreamWriterFactory(bytePool, charPool);

            return new OutputFormatterWriteContext(
                context,
                writerFactory.CreateWriter,
                handlerResult.ResponseType,
                handlerResult.Response);
        }
    }
}

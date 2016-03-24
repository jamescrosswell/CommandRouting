using System.Collections.Generic;
using System.Linq;
using CommandRouting.Handlers;
using CommandRouting.Helpers;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.MemoryPool;

namespace CommandRouting.Router.Serialization
{
    public static class OutputFormatSelector
    {
        internal static IOutputFormatter GetBestFormatter(
            this IEnumerable<IOutputFormatter> outputFormatters, OutputFormatterWriteContext context)
        {
            Ensure.NotNull(outputFormatters, nameof(outputFormatters));

            // Convert to an array, to avoid multiple enumeration
            var formatters = outputFormatters as IOutputFormatter[] ?? outputFormatters.ToArray();

            // Now get the best formatter (we'll use the first one if we can't find anything better)
            IOutputFormatter bestFormatter = formatters.FirstOrDefault(x => x.CanWriteResult(context));
            return bestFormatter ?? formatters.FirstOrDefault();
        }

        internal static OutputFormatterWriteContext OutputFormatterContext(this HttpContext context, IHandlerResult handlerResult)
        {
            IArraySegmentPool<byte> byteSegmentPool = new DefaultArraySegmentPool<byte>();
            IArraySegmentPool<char> charSegmentPool = new DefaultArraySegmentPool<char>();
            IHttpResponseStreamWriterFactory writerFactory = new MemoryPoolHttpResponseStreamWriterFactory(byteSegmentPool, charSegmentPool);

            return new OutputFormatterWriteContext(
                context,
                writerFactory.CreateWriter,
                handlerResult.ResponseType,
                handlerResult.Response);
        }
    }
}

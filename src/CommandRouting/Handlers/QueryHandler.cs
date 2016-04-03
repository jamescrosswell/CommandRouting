using System.IO;

namespace CommandRouting.Handlers
{
    /// <summary>
    /// Base class for "Query" type handlers (i.e. request handlers that return a result)
    /// </summary>
    /// <typeparam name="TRequest">The type of the request model that the handler can process</typeparam>
    /// <typeparam name="TResponse">The type of the response that the handler provides</typeparam>
    public abstract class QueryHandler<TRequest, TResponse> : IQueryHandler<TRequest, TResponse>
    {
        public abstract HandlerResult Dispatch(TRequest request);

        /// <summary>
        /// Helper function that makes returning NotHandled result
        /// </summary>
        /// <returns>A NotHandled result</returns>
        protected HandlerResult Continue()
        {
            return new NotHandled();
        }

        /// <summary>
        /// Helper function to make it easier to return a handled result
        /// </summary>
        protected HandlerResult Handled(TResponse response)
        {
            return new Handled<TResponse>(response);
        }

        protected FileResult File(Stream stream, string contentType, string fileName = null)
        {
            return new FileResult(stream, contentType, fileName);
        }
    }

    /// <summary>
    /// Base class for query handlers that can service queries without any request details
    /// </summary>
    /// <typeparam name="TResponse">The type of the response that the handler provides</typeparam>
    public abstract class QueryHandler<TResponse> : QueryHandler<Unit, TResponse>, IQueryHandler<TResponse>
    {

    }
}

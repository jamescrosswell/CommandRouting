using System.Net;

namespace CommandRouting.Handlers
{
    public class HttpResponse<TResponse> : Handled<TResponse>, IHttpResponse
    {
        public HttpResponse(TResponse response, HttpStatusCode status) : base(response)
        {
            Status = status;
        }

        public HttpStatusCode Status { get; }
    }

    /// <summary>
    /// Simple override for an HttpResponse that doesn't serialise any response object to the message
    /// body. In this case we specify "Unit" as the response type (the functional null equivalent).
    /// </summary>
    public class HttpResponse : HttpResponse<Unit>
    {
        public HttpResponse(HttpStatusCode status) : base(Unit.Result, status)
        {
        }

        /// <summary>
        /// Implicitly convert from an HttpStatusCode so that developers can return
        /// HttpStatusCodes in their request handlers.
        /// </summary>
        /// <param name="status"></param>
        public static implicit operator HttpResponse(HttpStatusCode status)
        {
            return new HttpResponse(status);
        }
    }
}

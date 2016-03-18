using System.Net;

namespace CommandRouting.Handlers
{
    public class HttpResponseResult<TResponse> : Handled<TResponse>, IHttpResponseResult
    {
        public HttpResponseResult(TResponse response, HttpStatusCode status) : base(response)
        {
            Status = status;
        }

        public HttpStatusCode Status { get; }
    }

    // Simple override for HttpResponseResults that don't serialise any response object to the message body
    public class HttpResponseResult : HttpResponseResult<Unit>
    {
        public HttpResponseResult(HttpStatusCode status) : base(Unit.Result, status)
        {
        }
    }
}
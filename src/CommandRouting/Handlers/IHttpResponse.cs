using System.Net;

namespace CommandRouting.Handlers
{
    /// <summary>
    /// An interface for HandlerResults that return an explicit HttpStatusCode. A 
    /// HandledResult that doesn't implement this interface implicitly returns 200 OK.
    /// </summary>
    public interface IHttpResponse: IHandlerResult
    {
        HttpStatusCode Status { get; }
    }
}
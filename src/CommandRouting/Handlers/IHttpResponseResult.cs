using System.Net;

namespace CommandRouting.Handlers
{
    public interface IHttpResponseResult: IHandlerResult
    {
        HttpStatusCode Status { get; }
    }
}
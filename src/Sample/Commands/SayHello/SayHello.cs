using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    public class SayHello : QueryHandler<SayHelloRequest, string>
    {
        public override HandlerResult Dispatch(SayHelloRequest request)
        {
            return Handled($"Hello {request.Name}");
        }
    }
}

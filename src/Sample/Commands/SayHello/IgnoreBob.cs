using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    public class IgnoreBob : QueryHandler<SayHelloRequest, string>
    {
        public override HandlerResult Dispatch(SayHelloRequest request)
        {
            return request.Name.ToLowerInvariant() == "bob" 
                ? Handled($"I don't want to talk to you { request.Name }") 
                : Continue();
        }
    }
}
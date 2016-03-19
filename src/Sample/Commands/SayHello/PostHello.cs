using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    public class PostHello : CommandHandler<SayHelloRequest, string>
    {
        public override HandlerResult Dispatch(SayHelloRequest request)
        {
            return Handled($"Drop me a line {request.Name}");
        }
    }
}

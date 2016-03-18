using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    [CommandPipeline(typeof(IgnoreBob))]
    public class SayHello : CommandHandler<SayHelloRequest, string>
    {
        public override HandlerResult Dispatch(SayHelloRequest request)
        {
            return Handled($"Hello {request.Name}");
        }
    }
}

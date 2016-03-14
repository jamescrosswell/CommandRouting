using CommandRouting;
using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    public class SayHello
    {
        public string Name { get; set; }
    }

    public class IgnoreBob : CommandHandler<SayHello, string>
    {
        public override CommandHandlerResult Dispatch(SayHello request)
        {
            return request.Name == "Bob" 
                ? Handled($"I don't want to talk to you { request.Name }") 
                : Continue();
        }
    }

    [CommandPipeline(typeof(IgnoreBob))]
    public class SayHelloCommand : ICommand<SayHello, string>
    {
        public string Execute(SayHello request)
        {
            return $"Hello {request.Name}";
        }
    }
}

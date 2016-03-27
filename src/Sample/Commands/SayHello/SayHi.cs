using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    public class SayHi : CommandHandler<SayHiRequest, string>
    {
        public override HandlerResult Dispatch(SayHiRequest request)
        {
            return Handled($"Hi {request.Nickname}");
        }
    }
}
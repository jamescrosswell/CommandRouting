using CommandRouting.Handlers;

namespace Sample.Commands.SayHello
{
    public class SayHi : QueryHandler<SayHiRequest, string>
    {
        public override HandlerResult Dispatch(SayHiRequest request)
        {
            return Handled($"Hi {request.Nickname}");
        }
    }
}
using CommandRouting;
using CommandRouting.Handlers;

namespace Sample.Commands.Account
{
    public class SignOut : QueryHandler<string>
    {
        public override HandlerResult Dispatch(Unit request)
        {
            return Handled("Goodbye");
        }
    }
}
using CommandRouting.Handlers;

namespace Sample.Commands.Account
{
    public class SignOut : CommandHandler<EmptyRequest, string>
    {
        public override HandlerResult Dispatch(EmptyRequest request)
        {
            return Handled("Goodbye");
        }
    }
}
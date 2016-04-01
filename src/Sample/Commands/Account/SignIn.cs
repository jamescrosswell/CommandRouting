using CommandRouting.Handlers;

namespace Sample.Commands.Account
{
    public class SignIn : CommandHandler<EmptyRequest, string>
    {
        public override HandlerResult Dispatch(EmptyRequest request)
        {
            return Handled("Hello");
        }
    }
}
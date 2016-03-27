using CommandRouting;
using CommandRouting.Config;
using CommandRouting.Handlers;

namespace Sample.CommandSets.Account
{
    public class AccountCommands: ICommandSet
    {
        public void Configure(ICommandRouteBuilder routes)
        {
            routes
                .Post("signin")
                .As<EmptyRequest>()
                .RoutesTo<SignIn>();

            routes
                .Delete("signout")
                .As<EmptyRequest>()
                .RoutesTo<SignOut>();
        }
    }

    public class SignIn : CommandHandler<EmptyRequest, string>
    {
        public override HandlerResult Dispatch(EmptyRequest request)
        {
            return Handled("Hello");
        }
    }

    public class SignOut : CommandHandler<EmptyRequest, string>
    {
        public override HandlerResult Dispatch(EmptyRequest request)
        {
            return Handled("Goodbye");
        }
    }

    public class EmptyRequest
    {
        
    }

}

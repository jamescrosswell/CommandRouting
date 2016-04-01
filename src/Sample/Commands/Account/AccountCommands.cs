using CommandRouting;
using CommandRouting.Config;

namespace Sample.Commands.Account
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

    public class EmptyRequest
    {
        
    }

}

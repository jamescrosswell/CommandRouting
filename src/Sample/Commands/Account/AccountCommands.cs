﻿using CommandRouting;
using CommandRouting.Config;

namespace Sample.Commands.Account
{
    public class AccountCommands: IRouteSet
    {
        public void Configure(ICommandRouteBuilder routes)
        {
            routes
                .Post("signin")
                .As<Unit>()
                .RoutesTo<SignIn>();

            routes
                .Delete("signout")
                .As<Unit>()
                .RoutesTo<SignOut>();
        }
    }

}

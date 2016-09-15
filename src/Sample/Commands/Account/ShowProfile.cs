using CommandRouting;
using CommandRouting.Handlers;

namespace Sample.Commands.Account
{
    public class ShowProfile : QueryHandler<Unit, Profile>
    {
        public override HandlerResult Dispatch(Unit request)
        {
            return Handled(new Profile{
                Name = "Old Man Boo",
                Age = 101
                });
        }
    }

    public class Profile
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
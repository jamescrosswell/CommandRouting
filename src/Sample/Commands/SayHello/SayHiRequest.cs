using CommandRouting.Router;

namespace Sample.Commands.SayHello
{
    [GetRouteRequest("hello/{name:alpha}/{nickname:alpha}", typeof(IgnoreBob), typeof(SayHi))]
    public class SayHiRequest : SayHelloRequest
    {
        public string Nickname { get; set; }
    }
}
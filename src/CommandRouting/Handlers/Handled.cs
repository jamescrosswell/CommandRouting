using System;

namespace CommandRouting.Handlers
{
    public class Handled<TResponse>: HandlerResult
    {
        public override object Response { get; }
        public override Type ResponseType => typeof (TResponse);

        public Handled(TResponse response) : base(true)
        {
            Response = response;
        }
    }

    public class Handled : Handled<Unit>
    {
        public Handled() : base(Unit.Result)
        {
        }
    }
}
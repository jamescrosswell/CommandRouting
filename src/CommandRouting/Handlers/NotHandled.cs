using System;

namespace CommandRouting.Handlers
{
    public class NotHandled : HandlerResult
    {
        public override object Response => Unit.Result;
        public override Type ResponseType => typeof(Unit);

        public NotHandled() : base(false)
        {
        }
    }
}
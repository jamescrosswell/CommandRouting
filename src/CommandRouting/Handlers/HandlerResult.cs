using System;
using System.Dynamic;
using System.Net;

namespace CommandRouting.Handlers
{
    public abstract class HandlerResult : IHandlerResult
    {
        public abstract object Response { get; }
        public abstract Type ResponseType { get; }

        protected HandlerResult(bool isHandled)
        {
            IsHandled = isHandled;
        }
        public bool IsHandled { get; }
    }
}
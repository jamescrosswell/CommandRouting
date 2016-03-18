using System;

namespace CommandRouting.Handlers
{
    public interface IHandlerResult
    {
        object Response { get; }
        Type ResponseType { get; }
        bool IsHandled { get; }
    }
}
using System;

namespace CommandRouting.Handlers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class CommandHandlerAttribute : Attribute
    {
        public CommandHandlerAttribute(Type handlerType)
        {
        }
    }
}

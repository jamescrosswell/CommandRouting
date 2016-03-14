using System;

namespace CommandRouting.Handlers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class CommandPipelineAttribute : Attribute
    {
        public CommandPipelineAttribute(Type handlerType)
        {
        }
    }
}

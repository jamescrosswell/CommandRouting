using System;
using System.Linq;
using CommandRouting.Handlers;

namespace CommandRouting.Helpers
{
    public static class CommandHelper
    {
        private static Type[] CommandTypeArguments(Type commandType)
        {
            var genericCommandInterface = commandType
                .GetInterfaces()
                .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
            return genericCommandInterface.GetGenericArguments();
        }

        public static Type GetCommandRequestType<TCommand>()
            where TCommand: ICommandHandler
        {
            return CommandTypeArguments(typeof(TCommand)).First();
        }

        public static Type GetCommandResponseType<TCommand>()
            where TCommand : ICommandHandler
        {
            return CommandTypeArguments(typeof(TCommand)).Last();
        }
    }
}
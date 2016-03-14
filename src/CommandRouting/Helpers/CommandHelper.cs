using System;
using System.Linq;

namespace CommandRouting.Helpers
{
    public static class CommandHelper
    {
        private static Type[] CommandTypeArguments(Type commandType)
        {
            var genericCommandInterface = commandType
                .GetInterfaces()
                .First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommand<,>));
            return genericCommandInterface.GetGenericArguments();
        }

        public static Type GetCommandRequestType<TCommand>()
            where TCommand: ICommand
        {
            return CommandTypeArguments(typeof(TCommand)).First();
        }

        public static Type GetCommandResponseType<TCommand>()
        {
            return CommandTypeArguments(typeof(TCommand)).Last();
        }
    }
}
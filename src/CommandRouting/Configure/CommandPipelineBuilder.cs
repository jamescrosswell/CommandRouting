using System;
using CommandRouting.Handlers;

namespace CommandRouting.Configure
{
    public class CommandPipelineBuilder<TRequest>
    {
        internal RoutePipelineBuilder RoutePipelineBuilder;

        public CommandPipelineBuilder(RoutePipelineBuilder routePipelineBuilder)
        {
            RoutePipelineBuilder = routePipelineBuilder;
        }

        /// <summary>
        /// Regsiters a pipeline with the <see cref="CommandRouteBuilder"/>
        /// </summary>
        /// <param name="commandHandlerTypes"></param>
        private void RegisterRoute(params Type[] commandHandlerTypes)
        {
            RoutePipelineBuilder.CommandRouteBuilder.AddRoute<TRequest>(
                RoutePipelineBuilder.Verb,
                RoutePipelineBuilder.RouteTemplate,
                commandHandlerTypes
                );
        }

        /// <summary>
        /// You probably shouldn't use this as it isn't strongly typed (if you try to build
        /// a command pipeline of types that are not command handlers it will fail at runtime).
        /// The only reason I provided it is because I couldn't handle all possible scenarios
        /// with the generic To overrides (e.g. if you want more than 9 handlers in a pipeline).
        /// </summary>
        /// <param name="handlers"></param>
        public void RoutesTo(params Type[] handlers)
        {
            RegisterRoute(handlers);
        }

        public void RoutesTo<THandler01>()
            where THandler01 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01)
                );
        }

        public void RoutesTo<THandler01, THandler02>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03, THandler04>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
            where THandler04 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03), typeof(THandler04)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03, THandler04, THandler05>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
            where THandler04 : ICommandHandler<TRequest>
            where THandler05 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03), typeof(THandler04), typeof(THandler05)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03, THandler04, THandler05, THandler06>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
            where THandler04 : ICommandHandler<TRequest>
            where THandler05 : ICommandHandler<TRequest>
            where THandler06 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03), typeof(THandler04), typeof(THandler05),
                typeof(THandler06)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03, THandler04, THandler05, THandler06, THandler07>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
            where THandler04 : ICommandHandler<TRequest>
            where THandler05 : ICommandHandler<TRequest>
            where THandler06 : ICommandHandler<TRequest>
            where THandler07 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03), typeof(THandler04), typeof(THandler05),
                typeof(THandler06), typeof(THandler07)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03, THandler04, THandler05, THandler06, THandler07, THandler08>()
            where THandler01 : ICommandHandler<TRequest>
            where THandler02 : ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
            where THandler04 : ICommandHandler<TRequest>
            where THandler05 : ICommandHandler<TRequest>
            where THandler06 : ICommandHandler<TRequest>
            where THandler07 : ICommandHandler<TRequest>
            where THandler08 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03), typeof(THandler04), typeof(THandler05),
                typeof(THandler06), typeof(THandler07), typeof(THandler08)
                );
        }

        public void RoutesTo<THandler01, THandler02, THandler03, THandler04, THandler05, THandler06, THandler07, THandler08, THandler09>()
            where THandler01: ICommandHandler<TRequest>
            where THandler02: ICommandHandler<TRequest>
            where THandler03 : ICommandHandler<TRequest>
            where THandler04 : ICommandHandler<TRequest>
            where THandler05 : ICommandHandler<TRequest>
            where THandler06 : ICommandHandler<TRequest>
            where THandler07 : ICommandHandler<TRequest>
            where THandler08 : ICommandHandler<TRequest>
            where THandler09 : ICommandHandler<TRequest>
        {
            RegisterRoute(
                typeof(THandler01), typeof(THandler02), typeof(THandler03), typeof(THandler04), typeof(THandler05), 
                typeof(THandler06), typeof(THandler07), typeof(THandler08), typeof(THandler09)
                );
        }
    }
}
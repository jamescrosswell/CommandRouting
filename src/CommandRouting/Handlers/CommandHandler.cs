namespace CommandRouting.Handlers
{
    /// <summary>
    /// Base class for "Request" stype handlers (i.e. command handlers that return a result)
    /// </summary>
    /// <typeparam name="TRequest">The type of the request model that the handler can process</typeparam>
    /// <typeparam name="TResponse">The type of the response that the handler provides</typeparam>
    public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
    {
        public abstract HandlerResult Dispatch(TRequest request);

        /// <summary>
        /// Helper function that makes returning NotHandled result
        /// </summary>
        /// <returns>A NotHandled result</returns>
        protected HandlerResult Continue()
        {
            return new NotHandled();
        }

        /// <summary>
        /// Helper function to make it easier to return a handled result
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected HandlerResult Handled(TResponse response)
        {
            return new Handled<TResponse>(response);
        }
    }

    /// <summary>
    /// Base class for command handlers that do not need to return a result - instead they return
    /// "Unit" (the functional equivalent of null)
    /// </summary>
    /// <typeparam name="TRequest">The type of the request model that the class handles</typeparam>
    public abstract class CommandHandler<TRequest> : CommandHandler<TRequest, Unit>
    {
        protected HandlerResult Handled()
        {
            return base.Handled(Unit.Result);
        }
    }
}
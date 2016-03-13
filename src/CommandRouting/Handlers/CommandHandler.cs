namespace CommandRouting.Handlers
{
    public abstract class CommandHandler<TRequest, TResponse>
    {
        public abstract CommandHandlerResult Dispatch(TRequest request);

        public const int DefaultOrder = 100;

        /// <summary>
        /// Specifies the order in which the command handler should be dispatched
        /// </summary>
        public virtual int Order => DefaultOrder;

        /// <summary>
        /// Helper function that makes returning NotHandled result
        /// </summary>
        /// <returns>A NotHandled result</returns>
        protected CommandHandlerResult Continue()
        {
            return new NotHandled();
        }

        /// <summary>
        /// Helper function to make it easier to return a handled result
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected CommandHandlerResult Handled(TResponse response)
        {
            return new Handled<TResponse>(response);
        }
    }
}
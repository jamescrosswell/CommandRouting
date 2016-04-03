namespace CommandRouting.Handlers
{
    /// <summary>
    /// Base class for command handlers that do not need to return a result - instead they return
    /// "Unit" (the functional equivalent of null)
    /// </summary>
    /// <typeparam name="TRequest">The type of the request model that the class handles</typeparam>
    public abstract class CommandHandler<TRequest> : QueryHandler<TRequest, Unit>, ICommandHandler<TRequest>
    {
        protected HandlerResult Handled()
        {
            return base.Handled(Unit.Result);
        }
    }

}
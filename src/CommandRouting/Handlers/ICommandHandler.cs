namespace CommandRouting.Handlers
{
    public interface IRequestHandler<in TRequest> 
    {
        HandlerResult Dispatch(TRequest request);
    }

    /// <summary>
    /// Requests that require a response are Queries in CQRS terms... these get handled by Query Handlers.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request model that the handler can process</typeparam>
    /// <typeparam name="TResponse">The type of the response that the handler provides</typeparam>
    public interface IQueryHandler<in TRequest, out TResponse> : IRequestHandler<TRequest>
    {
    }

    /// <summary>
    /// Some queries have no request information... they just issue a response
    /// </summary>
    /// <typeparam name="TResponse">The type of the response that the handler provides</typeparam>
    public interface IQueryHandler<out TResponse> : IRequestHandler<Unit>
    {
    }

    /// <summary>
    /// Commands, in CQRS are requests that do not return a response... they do stuff instead. These get
    /// handled by command handlers.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request model that the handler can process</typeparam>
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest>
    {
    }
}
namespace CommandRouting.Handlers
{
    public interface ICommandHandler<in TRequest> 
    {
        HandlerResult Dispatch(TRequest request);
    }

    public interface ICommandHandler<in TRequest, out TResponse> : ICommandHandler<TRequest>
    {
    }
}
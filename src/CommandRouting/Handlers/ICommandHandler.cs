namespace CommandRouting.Handlers
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TRequest> : ICommandHandler
    {
        HandlerResult Dispatch(TRequest request);
    }

    public interface ICommandHandler<in TRequest, out TResponse> : ICommandHandler<TRequest>
    {
    }
}
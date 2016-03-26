namespace CommandRouting
{
    public interface ICommand<in TRequest, out TResponse>
    {
        TResponse Execute(TRequest request);
    }

    public interface ICommand<in TRequest> : ICommand<TRequest, Unit>
    {
        
    }
}

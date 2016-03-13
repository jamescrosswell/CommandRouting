namespace CommandRouting.Handlers
{
    public abstract class CommandHandlerResult
    {
        protected CommandHandlerResult(bool isHandled)
        {
            IsHandled = isHandled;
        }
        public bool IsHandled { get; }
    }
}
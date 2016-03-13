namespace CommandRouting.Handlers
{
    public class NotHandled : CommandHandlerResult        
    {
        public NotHandled() : base(false)
        {
        }
    }
}
namespace CommandRouting.Handlers
{
    public class Handled<TResponse>: CommandHandlerResult        
    {
        public TResponse Response { get; set; }

        public Handled(TResponse response) : base(true)
        {
            Response = response;
        }
    }
}
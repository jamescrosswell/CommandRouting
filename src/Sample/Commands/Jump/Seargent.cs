using CommandRouting.Handlers;

namespace Sample.Commands.Jump
{
    public class Seargent : CommandHandler<JumpRequest>
    {
        private readonly JumpContext _context;

        public Seargent(JumpContext context)
        {
            _context = context;
        }

        public override HandlerResult Dispatch(JumpRequest request)
        {
            // The seargent just clarifies orders for other request handlers in the pipeline... 
            _context.Height = 2.4m;

            // he never handles the command
            return Continue();
        }
    }
}

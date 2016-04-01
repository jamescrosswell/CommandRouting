using CommandRouting.Handlers;

namespace Sample.Commands.Jump
{
    public class Private: QueryHandler<JumpRequest, string>
    {
        private readonly JumpContext _context;

        public Private(JumpContext context)
        {
            _context = context;
        }

        public override HandlerResult Dispatch(JumpRequest request)
        {
            // The private tries to follow orders, but sometimes he needs a bit of context 
            // in order to be able to do this... this context needs to come from another handler
            // in the pipeline (such as the Seargent
            return (_context?.Height > 0m)
                ? Handled($"{_context.Height} meters, yes sir!")
                : Handled("How high?");
        }
    }
}

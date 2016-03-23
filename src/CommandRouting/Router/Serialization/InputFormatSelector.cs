using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Formatters;

namespace CommandRouting.Router.Serialization
{
    public interface IInputFormatSelector
    {
        IInputFormatter GetFormatterForContext(InputFormatterContext context);
    }

    public class InputFormatSelector : IInputFormatSelector
    {
        private readonly IEnumerable<IInputFormatter> _inputFormatters;

        public InputFormatSelector(IEnumerable<IInputFormatter> inputFormatters)
        {
            if (inputFormatters == null)
                throw new ArgumentNullException(nameof(inputFormatters));

            _inputFormatters = inputFormatters;
        }

        public IInputFormatter GetFormatterForContext(InputFormatterContext context)
        {
            IInputFormatter bestFormatter = _inputFormatters.FirstOrDefault(x => x.CanRead(context));
            return bestFormatter ?? _inputFormatters.FirstOrDefault();
        }
    }
}

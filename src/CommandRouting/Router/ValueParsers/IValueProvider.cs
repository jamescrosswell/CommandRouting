using System.Collections.Generic;

namespace CommandRouting.Router.ValueParsers
{
    public interface IValueParser
    {
        void ParseValues(object requestModel);
    }
}
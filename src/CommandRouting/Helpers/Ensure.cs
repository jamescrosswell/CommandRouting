using System;

namespace CommandRouting.Helpers
{
    public static class Ensure
    {
        public static void NotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }
    }
}

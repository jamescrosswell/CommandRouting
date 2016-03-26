namespace CommandRouting.Helpers
{
    public static class StringHelper
    {
        public static string ForceTrailing(this string text, string postfix)
        {
            text = text ?? "";
            return (text.EndsWith(postfix))
                ? text
                : text + postfix;
        }

        public static string ForceLeading(this string text, string prefix)
        {
            text = text ?? "";
            return (text.StartsWith(prefix))
                ? text
                : prefix + text;
        }

        public static bool IsBlank(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotBlank(this string text)
        {
            return !text.IsBlank();
        }

        public static string StripLeading(this string text, string prefix)
        {
            // If the original string is null then don't change it
            if (text == null)
                return null;

            return (text.StartsWith(prefix))
                ? text.Remove(0, prefix.Length)
                : text;
        }

        public static string StripTrailing(this string text, string postfix)
        {
            // If the original string is null then don't change it
            if (text == null)
                return null;

            return (text.EndsWith(postfix))
                ? text.Remove(text.Length - postfix.Length)
                : text;
        }

        public static string Strip(this string text, string bookend)
        {
            return text.StripLeading(bookend).StripTrailing(bookend);
        }
    }
}
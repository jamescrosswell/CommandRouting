using System.Collections.Generic;

namespace CommandRouting.Helpers
{
    public static class ListHelper
    {
        /// <summary>
        /// Convenience extension to easily convert individual items into trivial 
        /// enumerable arrays of one...
        /// </summary>
        /// <typeparam name="T">The type of the item that we want to be enumerable</typeparam>
        /// <param name="singleItem">The item to wrap in an array</param>
        /// <returns>An array containing the <paramref name="singleItem"/></returns>
        public static IEnumerable<T> ArrayOfOne<T>(this T singleItem)
        {
            return new T[] {singleItem};
        }
    }
}

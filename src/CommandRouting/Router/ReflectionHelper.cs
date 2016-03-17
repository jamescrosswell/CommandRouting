using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CommandRouting.Router
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Sets a deep/nested property on a complex object. For example you could use a property name like
        /// "Address.ZipCode" to set the postal code property in an "Address" composite object on an object.
        /// </summary>
        /// <param name="obj">The object who's deep property needs setting</param>
        /// <param name="deepName">The deep property name to be set</param>
        /// <param name="valueAsString">A string representation of the value to assign to the property</param>
        /// <returns>True if the property was set, false otherwise (typicalty that means it doesn't exist)</returns>
        public static bool TrySetDeepPropertyStringValue(this object obj, string deepName, string valueAsString)
        {
            Action<PropertyInfo, object> setConvertedValue = (property, target) =>
            {
                object value = property.ParseValue(valueAsString);
                property.SetValue(target, value);
            };
            return ManipulateDeepProperty(obj, deepName, setConvertedValue);
        }

        public static object ParseValue(this PropertyInfo property, string value)
        {
            return Convert.ChangeType(value, property.PropertyType);
        }

        private static bool ManipulateDeepProperty(object source, string deepName, Action<PropertyInfo, object> operation)
        {
            // If the object is null or the deepName is gibberish then return false and don't do anything
            string[] parts = deepName.Split(new char[] { '.' }, 2);
            if (source == null || parts.Length == 0)
                return false;

            // Get the type info for the next property to handle
            PropertyInfo info = source.GetType().GetProperty(parts[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (info == null)
                return false;

            // If this is a leaf node then perform our operation on the property
            if (parts.Length == 1)
            {
                try
                {
                    // TODO: We could probably log this in some kind of binding errors collection
                    operation(info, source);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            // Otherwise, get the value of the property and recursively perform the operation on 
            // the remaining child nodes in the deep property chain
            object child = info.GetValue(source, null);
            return ManipulateDeepProperty(child, parts[1], operation);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Extensions
{
    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T>(this T item, string propName)
        {
            return item.GetType().GetProperty(propName).GetValue(item, null).ToString();
        }
    }
}

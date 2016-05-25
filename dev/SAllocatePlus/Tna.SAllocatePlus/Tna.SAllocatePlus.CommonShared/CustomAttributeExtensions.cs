using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Tna.SAllocatePlus.CommonShared
{
    public static class CustomAttributeExtensions
    {
        /// <summary>
        /// Get attribute of the entered type. Return the attribute if found or null if not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pi"></param>
        /// <param name="inherit"></param>
        /// <returns>The attribute if found or null if not found</returns>
        public static T GetAttribute<T>(this PropertyInfo pi, bool inherit = true) where T : Attribute
        {
            var attrs = pi.GetCustomAttributes(typeof(T), inherit);
            if (attrs != null && attrs.Length > 0)
            {
                return attrs[0] as T;
            }

            return null;
        }

        public static bool HasAttribute<T>(this PropertyInfo pi, bool inherit = true) where T:Attribute
        {
            return GetAttribute<T>(pi, inherit) != null;
        }
    }
}

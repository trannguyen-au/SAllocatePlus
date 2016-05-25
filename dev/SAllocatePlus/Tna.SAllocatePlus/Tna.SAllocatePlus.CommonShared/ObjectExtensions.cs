using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tna.SAllocatePlus.CommonShared
{
    public static class ObjectExtensions
    {
        public static T MergeTo<T>(this object from, T target, params string[] skipProperty) where T : class
        {
            // get all property of origin object
            if (from == null) return target;

            var fromPInfos = from.GetType().GetProperties();
            var toPInfos = typeof(T).GetProperties();

            foreach (var fpi in fromPInfos)
            {
                // skip if source field cannot be read or ignored or specified as skiped 
                if (!fpi.CanRead || (fpi.HasAttribute<CopyOptionAttribute>() && fpi.GetAttribute<CopyOptionAttribute>().Ignore)) continue;
                if (skipProperty != null && skipProperty.Contains(fpi.Name)) continue;

                foreach (var tpi in toPInfos)
                {
                    // skip if field cannot write or ignored.
                    if (!tpi.CanWrite ||
                        (tpi.HasAttribute<CopyOptionAttribute>() && tpi.GetAttribute<CopyOptionAttribute>().Ignore)) continue;

                    // skip if field name is not match either from the Copy Option or using original name
                    if (tpi.HasAttribute<CopyOptionAttribute>())
                    {
                        var attrOption = tpi.GetAttribute<CopyOptionAttribute>();
                        if (!string.IsNullOrWhiteSpace(attrOption.MapFromFieldName) &&
                            fpi.Name != attrOption.MapFromFieldName) continue;
                    }
                    else if (fpi.Name != tpi.Name) continue;

                    var value = TryChangeValue(fpi.GetValue(from, null), fpi.PropertyType, tpi.PropertyType);

                    tpi.SetValue(target, value, null);
                }
            }

            return target;
        }

        private static object TryChangeValue(object value, Type from, Type to)
        {
            try
            {
                return Convert.ChangeType(value, to);
            }
            catch
            {
                if (value == null)
                {
                    if (!to.IsPrimitive)
                        return value;

                    if (to == typeof(int) || to == typeof(long) || to == typeof(short) || to == typeof(byte))
                    {
                        return 0;
                    }

                    if (to == typeof(decimal))
                    {
                        return 0.0m;
                    }

                    if (to == typeof(float) || to == typeof(double))
                    {
                        return 0.0f;
                    }

                    if (to == typeof(string) || to == typeof(char))
                    {
                        return null;
                    }

                    if (to == typeof(bool))
                    {
                        return false;
                    }
                }

                if (to == typeof(string))
                {
                    return value.ToString();
                }

                if (to == typeof(DateTime) || to == typeof(DateTime?))
                {
                    var result = Tools.TryParseDateTimeAuFormat(value.ToString());
                    return result.GetValueOrDefault();
                }
                if (to == typeof(int) ||
                    to == typeof(long) ||
                    to == typeof(short) ||
                    to == typeof(byte))
                {
                    var intValue = 0;
                    int.TryParse(value.ToString(), out intValue);
                    return intValue;
                }
                if (to == typeof(decimal))
                {
                    var decimalValue = 0m;
                    decimal.TryParse(value.ToString(), out decimalValue);
                    return decimalValue;
                }
                if (to == typeof(double) ||
                    to == typeof(float))
                {
                    var fValue = 0.0;
                    double.TryParse(value.ToString(), out fValue);
                    return fValue;
                }
                if (to == typeof(bool))
                {
                    if (from == typeof(int) ||
                        from == typeof(int?)) return (int)value != 0;
                    if (from != typeof(bool) &&
                        from != typeof(bool?)) return false; // force to handle this type by custom code out side
                    return (bool)value;
                }

                // nullable types
                if (from.IsGenericType && from.GetGenericTypeDefinition() == typeof(Nullable<>))
                {

                }

                return value;
            }
        }
    }
}

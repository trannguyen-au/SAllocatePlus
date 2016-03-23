using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace SwinSchool.DAL
{
    /// <summary>
    /// Data table extension to convert ADO data table into objects
    /// </summary>
    public static class DataTableExtensions
    {
        public static T ToObject<T>(this DataRow row)
        {
            T theObject = Activator.CreateInstance<T>();
            foreach (var pi in typeof(T).GetProperties())
            {
                SetValueForProperty<T>(theObject, pi, row);
            }

            return theObject;
        }

        private static void SetValueForProperty<T>(T theObject, PropertyInfo pi, DataRow row)
        {
            bool canMap = true;
            string columnName = pi.Name;
            if (!pi.CanWrite
                || pi.PropertyType == typeof(IEnumerable))
            {
                canMap = false;
            }
            else if (!row.Table.Columns.Contains(pi.Name))
            {
                // check the MapToColumn attribute if available
                var attr = pi.GetCustomAttributes(typeof(MapToColumnAttribute), true);
                if (attr.Length == 0) canMap = false;
                else
                {
                    columnName = (attr[0] as MapToColumnAttribute).ColumnName;
                }
            }

            if (canMap) // this property can be mapped
            {
                var valueFromTableCell = row[columnName];

                // cannot set null to valued data type so ignore these properties
                if (Convert.IsDBNull(valueFromTableCell)) valueFromTableCell = null;

                if (pi.PropertyType.IsGenericType && Nullable.GetUnderlyingType(pi.PropertyType) != null)
                {
                    pi.SetValue(theObject, valueFromTableCell, null);
                }
                else if (!(valueFromTableCell == null && pi.PropertyType.IsValueType))
                {
                    // convert to the correct type
                    var value = Convert.ChangeType(valueFromTableCell, pi.PropertyType);
                    pi.SetValue(theObject, value, null);
                }
            }
        }


        public static List<T> ToObjects<T>(this DataTable tbl)
        {
            List<T> result = new List<T>();
            foreach (DataRow row in tbl.Rows)
            {
                result.Add(row.ToObject<T>());
            }
            return result;
        }
    }
}

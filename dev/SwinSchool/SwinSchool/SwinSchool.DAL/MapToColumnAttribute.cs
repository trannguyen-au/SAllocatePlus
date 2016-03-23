using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapToColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public bool IsSkipExport { get; set; }
        public MapToColumnAttribute(string columnName, bool skipExport = false)
        {
            ColumnName = columnName;
            IsSkipExport = skipExport;
        }
    }
}

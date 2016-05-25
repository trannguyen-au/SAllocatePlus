using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.CommonShared
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CopyOptionAttribute: Attribute
    {
        public bool Ignore { get; set; }
        public string MapFromFieldName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    public class SortableAttribute : Attribute {
        public string GroupCloumn { get; set; }
        public SortableAttribute(string groupCloumn) {
            GroupCloumn = groupCloumn;
        }
    }
}

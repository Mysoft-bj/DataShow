using My.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations.Schema
{



    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HasManyAttribute : Attribute
    {
        public string ParentKey { get; set; }
        public string OrderBy { get; set; }
        public Type ItemType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HasMany2ManyAttribute : Attribute
    {
        public HasMany2ManyAttribute(string midTable)
        {
            MidTable = midTable;
        }
        public HasMany2ManyAttribute()
        {

        }
        public string MidTable { get; set; }
        public string ParentKey { get; set; }
        public string ChildKey { get; set; }
        public string OrderBy { get; set; }
        public Type ItemType { get; set; }
    }

    public class HasOneAttribute : Attribute
    {
        public string ReferenceID { get; set; }
        public Type ItemType { get; set; }
    }


    

    public enum IDGeneratedType
    {
        GUID
    }

  
    public class IndexAttribute :Attribute
    {
      
        public bool IsUnique
        {
            get;
            set;
        }
    }
    
 
   
    
}

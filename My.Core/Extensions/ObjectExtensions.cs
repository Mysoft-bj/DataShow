using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace My.Core.Format
{
   
    public static class ObjectExtensions
    {

       public static string ToPercent(this object num){        
           return string.Format( "{0:p}",num);
       }
       public static string ToNumber(this object num)
       {
           return string.Format("{0:n}", num);
       }
      
    }
}

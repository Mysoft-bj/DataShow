using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Core;
using Newtonsoft.Json;
namespace My.Core
{
   public class SearchModel
    {
       
       public int page
       {
           get;
           set;
       }
      
       public int size
       {
           get;
           set;
       }
       public SearchModel() {
           size = 15;
           page = 1;
          
       }
       public string Orderby { get; set; }
       [JsonProperty("q")]
       public string q { get; set; }
    
        public object[] v { get; set; }
        //public string GetQuery() {
        //    var prop = this.GetType().GetProperties();

        //}
    }

 
}

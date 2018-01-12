using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Core;
using Newtonsoft.Json;
using System.Web.Mvc;
namespace My.Application
{
    public class HttpResultWarp<T> {
        public T Result { get; set; }
        public string Message { get; set; }
    }
    public class HttpResult:ActionResult
    {

    
        public string Message { get; set; }
        [JsonProperty("result")]
        public object Result { get; set; }
        [JsonIgnore]
        public bool HasError { get { return !string.IsNullOrWhiteSpace(Message); } }
        public static HttpResult Error(string message)
        {
            HttpResult result = new HttpResult();
            result.Message = message;
          
            return result;
        }
        public static HttpResult OK()
        {
            HttpResult result = new HttpResult();          
            return result;
        }
        public static HttpResult OK( object data)
        {
          
            HttpResult result = new HttpResult();
            result.Result = data;
            return result;
        }
       
        public override string ToString()
        {
            return this.ToJson();
            //if (HasError) {
              
            //}
            //return this.Result.ToJson();
        }
        //public static implicit operator string(HttpResult or)
        //{

        //    return or.ToString();
        //}
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write(this.ToJson());
        }

    }

    public static class HttpResultExtentions { 
    public static HttpResult ToHttpResult(this object o){
        return HttpResult.OK(o);
    }
    }
}

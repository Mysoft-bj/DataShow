using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using My.Core;
using System.Linq.Expressions;
using System.Data;
using EmitMapper.Utils;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace My.Core
{
    public static class HttpHelper
    {
        public static T Get<T>(string url, dynamic header)
        {
            return default(T);
        }
        public static T Post<T>(string url, object data, object header=null)
        {

            var list = new List<string>();
            if (data != null)
            {
                foreach (var prop in data.GetType().GetProperties())
                {
                    list.Add(prop.Name + "=" + data.GetValue(prop.Name).ToString());

                }
            }
            WebClient client = new WebClient();
            byte[] postData = Encoding.UTF8.GetBytes(list.JoinAsString("&"));
            byte[] responseData = client.UploadData(url, "POST", postData);//得到返回字符流  
            var ret = Encoding.UTF8.GetString(responseData).ToObject<T>();
            return ret;
        }
        
    }
}

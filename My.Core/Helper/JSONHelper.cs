using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace My.Core
{
    public static class JSONHelper
    {
        //public static double ToFixed(this double d, int s)
        //{
        //    double sp = Math.Pow(10, s);
        //    return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
        //}
        static JSONCustomDateConverter _dateConverter = new JSONCustomDateConverter("yyyy-MM-dd HH:mm:ss");
        public static string ToJson(this object jsonObj, bool pretty=false)
        {
            if (jsonObj == null) return "";
            
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            if (pretty)
                jsetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json=JsonConvert.SerializeObject(jsonObj, Formatting.Indented, _dateConverter);
            return json;
        }
        public static string ToJson_New(this object jsonObj, bool pretty, bool isDelNullValue = false, bool isDelDefaultValue = false)
        {
            if (jsonObj == null) return "";
            if (!pretty && !isDelNullValue)
                return JsonConvert.SerializeObject(jsonObj);
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            if (pretty)
                jsetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            if (isDelNullValue)
                jsetting.NullValueHandling = NullValueHandling.Ignore;
            if (isDelDefaultValue)
                jsetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            return JsonConvert.SerializeObject(jsonObj, Formatting.Indented, jsetting);
        }
        public static T ToObject<T>(this string jsonstr)
        {
            if (string.IsNullOrEmpty(jsonstr))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(jsonstr);
        }

        public static List<T> ToObjectList<T>(this string jsonstr)
        {
            if (string.IsNullOrEmpty(jsonstr))
            {
                return new List<T>();
            }

            return JsonConvert.DeserializeObject<List<T>>(jsonstr) ?? new List<T>();
        }
    }

     public class JSONCustomDateConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
    {

        private string _dateFormat;

        public JSONCustomDateConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
          //   Newtonsoft.Json.Converters.JavaScriptDateTimeConverter
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(Nullable< DateTime>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteValue("null");
            else
                writer.WriteValue(Convert.ToDateTime(value).ToString(_dateFormat));
            writer.Flush();
        }
    }
}

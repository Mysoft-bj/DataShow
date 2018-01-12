using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using System.Web.Hosting;
namespace My.Core
{
    public static class StringExtensions
    {
        public static string MapPath(this string path) {
            if (path.Contains('/'))
                return HostingEnvironment.MapPath(path);
            return path;
        }
        public static long ParseLong(this string str) {
         return   long.Parse(str);
        }

        //guid not auto convert to string
        public static bool Equals(this string str, Guid guid)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (guid == null || guid == Guid.Empty)
                {
                    return true;
                }
                return false;

            }
            return str.Equals(guid.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool Equals(this Guid guid, string str)
        {
            return Equals(str, guid);
        }
       
        public static IEnumerable<T> Split<T>(this string source, char separator)
        {
            if (source.IsNullOrWhiteSpace())
                return new List<T>();
            var strArray = source.Trim(separator).Split(separator);
           
            var list = new List<T>();

            var converto = typeof(T);
             strArray.ForEach(item =>list.Add( (T)Convert.ChangeType(item, converto)));

        
            return list;
        }
        public static int ToInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            return int.Parse(str);
        }
       
        public static uint ToUInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            return uint.Parse(str);
        }
        public static string NoHtmlTag(this string source)
        {
            if (source.IsNullOrWhiteSpace())
                return source;
            //删除脚本
            var Htmlstring = Regex.Replace(source, @"<script[^>]*?>.*?</script>", "",
                RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            //Htmlstring.Replace("<", "");
            //Htmlstring.Replace(">", "");
            //Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        public static bool IsNotNull(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool GreaterEqual(this string source, string other)
        {

            return source.CompareTo(other) >= 0;
        }
        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool Greater(this string source, string other)
        {

            return source.CompareTo(other) > 0;
        }
        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool LessEqual(this string source, string other)
        {

            return source.CompareTo(other) <= 0;
        }
        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool Less(this string source, string other)
        {

            return source.CompareTo(other) < 0;
        }

        public static void ForEach<T>(this T[] source, Action<T> action)
        {
            if (source == null) return;
            foreach (T item in source)
            {
                action(item);
            }
        }

       
        public static T LoadFromFile<T>(this string filePath)
        {
            if (!File.Exists(filePath))
                return default(T);
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                var json = reader.ReadToEnd().Replace("\n", "").Replace("\r", "").Trim();
               return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}

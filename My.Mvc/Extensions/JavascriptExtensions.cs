using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Web.Caching;
using My.Core;
namespace System.Web.Mvc
{
    public static class JavascriptExtensions
    {
         static readonly string JsonWrap = "<script type=\"text/javascript\"> var {0} = {1} ;</script>\r\n";
         static readonly string JsFileWrap = "<script type=\"text/javascript\" src=\"{0}\"></script>\r\n";
        static readonly string CssFileWrap = "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" />\r\n";
        	
        public static IHtmlString RenderJSON(this HtmlHelper html, string varName, object obj)
        {
            string mcvscript = string.Empty;
            string json= JSONHelper.ToJson(obj);           
            if (varName.Contains("."))
                mcvscript = string.Format(JsonWrap, varName, json);
            else
                mcvscript = string.Format(JsonWrap, varName, json);

            return html.Raw(mcvscript);
        }          

        public static IHtmlString Enum2JSON(this HtmlHelper html, string varName, Type type)
        {
            return RenderJSON(html, varName, MyEnumHelper.GetList(type));

        }
        public static IHtmlString JsFile(this HtmlHelper html, string fileName)
        {
            return RenderFileWithVersion(html, fileName, JsFileWrap);
        }
        public static IHtmlString ServiceProxy(this HtmlHelper html,Type type)
        {
            
           
            var url = "/ApiService/Proxy?type=" + type.FullName+"&t="+DateTime.Now.Ticks;
            return html.Raw(string.Format(JsFileWrap, url));
            
            //return 
        }
        public static IHtmlString CssFile(this HtmlHelper html, string fileName)
        {
            return RenderFileWithVersion(html, fileName, CssFileWrap);
        }
         static IHtmlString RenderFileWithVersion(HtmlHelper html, string fileName,string fileWrap)
        {
           
            //if (fileName.StartsWith("~"))
            //{
            //    var area =(string) html.ViewContext.RouteData.DataTokens["area"];
            //    if (area.IsNotNull())
            //        fileName = fileName.Replace("~", "/Areas/" + area);
            
            //}

            string mcvscript = string.Empty;
            if (fileName.IndexOf('?') > 0)
            {
                mcvscript = string.Format(JsFileWrap, fileName);
            }
            else
            {
                var filewithverison = HttpRuntime.Cache[fileName];
                if (filewithverison == null)
                {
                    var filePath = PathHelper.MapPath(fileName);
                    var version = new FileInfo(filePath).LastWriteTime.ToString("yyyyMMddHHmmss");
                    filewithverison = fileName + "?" + version;
                    CacheDependency cdy = new CacheDependency(filePath);
                    HttpRuntime.Cache.Insert(fileName, filewithverison, cdy);

                }
                mcvscript = string.Format(fileWrap, filewithverison);
            }

            return html.Raw(mcvscript);
        }
    }
}

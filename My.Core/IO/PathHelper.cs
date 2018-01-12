using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;
using System.Web.Hosting;
namespace My.Core
{
    public static class PathHelper
    {
        public static string AppDataPath { get; private set; }
        public static string RootPath { get; private set; }
        public static string BinPath { get; private set; }
        static PathHelper() {
            RootPath = AppDomain.CurrentDomain.BaseDirectory.Trim('\\');
            BinPath = Path.Combine(RootPath, "bin");
            AppDataPath = Path.Combine(RootPath, "App_Data");
          
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }

        }
        public static string Combine(params string[] paths) {
            return Path.Combine(paths);
        }
        public static string GetDataPath(params string[] paths)
        {
            return Path.Combine(AppDataPath, Path.Combine(paths));
        }
        public static string MapPath(string virtualPath) {
            
            if (virtualPath.Contains('\\'))
                return virtualPath;
            return HostingEnvironment.MapPath(virtualPath);
        }
        public static string GetVirtualPath(string physicalpath)
        {
            var virtualPath = physicalpath.Replace(AppDomain.CurrentDomain.BaseDirectory, "/").Replace("\\", "/");
            return virtualPath;
        }
    }
}

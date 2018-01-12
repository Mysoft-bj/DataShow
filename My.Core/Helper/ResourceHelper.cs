using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;
namespace My.Core
{

    public static class ResourceHelper
    {
        public static string GetResourceString(string resourceName, Type type)
        {
            using (StreamReader reader = new StreamReader(GetResourceStream(resourceName, type), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }    
        public static Stream GetResourceStream(string resourceName,Type type)
        {           
            Assembly assembly = Assembly.GetAssembly(type);
            var stream = assembly.GetManifestResourceStream(resourceName);// resourcename包括资源的命名空间 
            if (stream == null)
            {
                var resName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(resourceName, StringComparison.CurrentCultureIgnoreCase));
                if (resName != null)
                    stream = assembly.GetManifestResourceStream(resName);
            }
            return stream;
        }    
       /// <summary>
       /// 
       /// </summary>
       /// <param name="type"></param>
       /// <param name="resourceName"></param>
        /// <param name="isOverWrite">如果文件存在,并且isOverWrite=false则抛出异常</param>
       /// <returns></returns>
        public static string CopyToAppData(Type type, string resourceName)
        {

            string configFile;
            configFile = Path.Combine(PathHelper.AppDataPath, resourceName);
            if (File.Exists(configFile)) {
                return configFile;
            }
            File.Delete(configFile);

            FileInfo fileinfo = new FileInfo(configFile);
            if (!Directory.Exists(fileinfo.DirectoryName))
            {
                Directory.CreateDirectory(fileinfo.DirectoryName);
            }
            using (var outputStream = new FileStream(configFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {

                using (var stream = GetResourceStream(resourceName, type))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    outputStream.Write(bytes, 0, bytes.Length);
                }
            }

            return configFile;
        }
    }
}

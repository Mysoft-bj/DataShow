using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace My.Core
{
    public static class FileHelper
    {
        public static void SaveString(string filePath, string content)
        {
            filePath = PathHelper.MapPath(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(content);
            }

        }
        public static string ReadString(string filePath)
        {
            filePath = PathHelper.MapPath(filePath);
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                return reader.ReadToEnd();               
            }
        }
        public static void DeleteIfExists(string filePath)
        {
            filePath = PathHelper.MapPath(filePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    public static class DirectoryHelper
    {        
        public static void CreateIfNotExists(string directory)
        {
            directory = PathHelper.MapPath(directory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}

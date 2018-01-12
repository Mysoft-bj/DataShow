using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace My.Core
{
   public static     class ImageHelper
    {
       public static string GetDataURL(string imgFile)
       {
           imgFile = imgFile.MapPath();
           return "<img src=\"data:image/"
                       + Path.GetExtension(imgFile).Replace(".", "")
                       + ";base64,"
                       + Convert.ToBase64String(File.ReadAllBytes(imgFile)) + "\" />";
       }
    }
}

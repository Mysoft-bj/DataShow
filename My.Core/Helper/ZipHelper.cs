using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace My.Core
{
  public static class ZipHelper
    {
      public static void UnZip(string zipFile, string destPath=null) {

          if (destPath.IsNullOrWhiteSpace()) {
              FileInfo fi = new FileInfo(zipFile);
              destPath = fi.DirectoryName;
          }
          if (!Directory.Exists(destPath)) {
              Directory.CreateDirectory(destPath);
          }
          using (FileStream zipFileToOpen = new FileStream(zipFile, FileMode.Open))
          using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Update))
              archive.ExtractToDirectory(destPath);
      }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
namespace My.Core
{
  public static  class MediaHelper
    {
      public static void ToMP4(string src, string dest)
      {

          var tempDest = dest + ".tmp.mp4";
          var ffmpeg = Path.Combine(PathHelper.RootPath, "lib\\ffmpeg\\");
          //  01.avi -vcodec copy -acodec mp2 01.mp4
          var commandArguments = "ffmpeg.exe -i {0}  {1}";
          var fi = new FileInfo(dest);
          if (!Directory.Exists(fi.DirectoryName))
              Directory.CreateDirectory(fi.DirectoryName);
          var command=ffmpeg + string.Format(commandArguments, src, tempDest);
          RunCmd(command);
          if (!File.Exists(tempDest))
          {
              var error = new Exception("操作失败，文件无法转码,执行命令:"+command);

              throw error;
          }
        
          Thread.Sleep(200);
          File.Move(tempDest, dest);

      }
      public static void RunCmd(string command)
      {
        
          //实例一个Process类，启动一个独立进程
          using (Process p = new Process())
          {
              //Process类有一个StartInfo属性
              //设定程序名
              p.StartInfo.FileName = "cmd.exe";
              //关闭Shell的使用  
              p.StartInfo.UseShellExecute = false;
              //设定程式执行参数   
              //  p.StartInfo.Arguments = "/c " + command;
              //重定向标准输入     
              p.StartInfo.RedirectStandardInput = true;
              p.StartInfo.RedirectStandardOutput = true;
             
              //设置不显示窗口
              p.StartInfo.CreateNoWindow = true;
              StringBuilder output = new StringBuilder();
              StringBuilder error = new StringBuilder();

              p.ErrorDataReceived += (sender, e) =>
              {
                  if (!String.IsNullOrEmpty(e.Data))
                  {
                      

                      // Add the text to the collected output.
                      error.Append(e.Data);
                  }
              };
              //启动
              p.Start();
              // Use a stream writer to synchronously write the sort input.
              StreamWriter writer = p.StandardInput;
              p.BeginOutputReadLine();
              writer.WriteLine(command);
              writer.WriteLine("exit");
              writer.Close();
              p.WaitForExit();
             
              //  var errorStr = error.ToString();
              //if (errorStr.IsNotNull())
              //{
              //    var exception = new Exception("命令行：" + command + "\r\n出错信息：" + errorStr);
              //    Logger.Error(exception);
              //}


          }
         
      }

     
    }
}

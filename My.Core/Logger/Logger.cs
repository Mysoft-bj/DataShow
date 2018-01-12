using System;
using log4net;
using System.IO;
using System.Globalization;
using System.Text;
using System.Diagnostics;
namespace My.Core
{
    
 
    public class Logger
    {
        private static readonly ILog _myLog4Net;
        static Logger()
        {
           
            string path = ResourceHelper.CopyToAppData(typeof(Logger), "log4net.xml");

            string xmlContent;
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                xmlContent = reader.ReadToEnd();
            }
            if (xmlContent.IndexOf("{{root}}") > 0)
            {
                xmlContent = xmlContent.Replace("{{root}}", PathHelper.Combine(PathHelper.RootPath, "log"));
                using (StreamWriter objWrite = new StreamWriter(path, false, Encoding.UTF8))
                {
                    objWrite.Write(xmlContent);
                }
            }
          log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
            _myLog4Net = LogManager.GetLogger("LogFile");

        }
      public   static void Init() {
          Logger.Warning("程序启用");
      }

        //public static void Debug(string message, params object[] args)
        //{
        //    var traceData = string.Format(CultureInfo.InvariantCulture, message, args);
        //    _myLog4Net.Debug(traceData);
        //}
        //public static void Info(string message, params object[] args)
        //{
        //    var traceData = string.Format(CultureInfo.InvariantCulture, message, args);
        //    _myLog4Net.Info(traceData);
        //}

        public static void Warning(string message, params object[] args)
        {
            var traceData = string.Format(CultureInfo.InvariantCulture, message, args);
            _myLog4Net.Warn(traceData);
        }

        public static void Error(string message, params object[] args)
        {
            var traceData = string.Format(CultureInfo.InvariantCulture, message, args);
            _myLog4Net.Error(traceData);
            Trace.TraceWarning(traceData);
        }
        public static void Error(Exception exception)
        {
            _myLog4Net.Error(exception);
            Trace.TraceWarning(exception.Message);
        }
        public static void Error(Exception exception, string message, params object[] args)
        {
            var traceData = string.Format(CultureInfo.InvariantCulture, message, args);
            _myLog4Net.Error(traceData, exception);
            Trace.TraceWarning(traceData);
        }

    }
}

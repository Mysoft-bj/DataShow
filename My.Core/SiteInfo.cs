using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
    public class SiteInfo : ISingletonWatcher
    {
        public  string WatcherFile
        {
            get { return "SiteInfo.json"; }
        }
        public string SiteName { get; set; }
        public string Host { get; set; }
        public string ResHost { get; set; }
        public string CopyRight { get; set; }   
        public string MailHost { get; set; }
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }
        public string RedisHost { get; set; }
        
       
    }
}

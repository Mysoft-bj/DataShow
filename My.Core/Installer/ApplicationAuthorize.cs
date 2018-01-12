using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace My.Core
{
    public class Authorize : ISingletonWatcher
    {
        public string WatcherFile
        {
            get { return "Authorize.json"; }
        }
        public Authorize()
        {
            RASKey = "AwEAAZ0mIVAmb2TJ5NZQT/jRKT3YNYluMAcdoV8OtEwR6xCk4TP2SgRH65mQ8w5X+Sfg6AmT+nQGrKy+ZM7vvSbk/WiOX5zIXIQpldYoBkuVoui4TcPxWGbRRxePkh52mfhG9z2MEFN+OEVW5Re4bHdjdvVrjKDBN4BvVK9kisexU2nz";
        }
        public string RASKey { get; private set; }

       
        public string Registry { get; set; }
    }

    public class AppAuthorizeManager
    {
        public static bool Authorize()
        {
            return true;
            var auth = IocManager.Resolve<Authorize>();
            var registryInfo = RSAHelper.DecryptString(auth.Registry, auth.RASKey);
          
            var cpuid = SystemInfoHelper.GetCpuId().JoinAsString();
            if (cpuid.Equals(registryInfo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            throw new Exception("系统未授权!");
        }
    }
}

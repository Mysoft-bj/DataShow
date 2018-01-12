using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
    public class SystemInfoHelper
    {
        public static List<string> GetCpuId()
        {
            List<string> cpuInfo = new List<string>();
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo.Add(mo.Properties["ProcessorId"].Value.ToString());
            }
            return cpuInfo.OrderBy(m => m).ToList(); ;
        }

    }
}

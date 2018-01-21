using My.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataShow.Core.Services
{
    public class DataShowCore
    {
        public void CreateDataShowAllJson(Guid EntitGUID) {

        }


        public void CreateDataShowPageJson(Guid EntityPageGUID)
        {
            

        }

        public void CreateDataModelJsonTree(List<DsEntityPageNodeData> PageList)
        {
            var maxLevel = PageList.Max(page => page.Level);



        }
    }


}

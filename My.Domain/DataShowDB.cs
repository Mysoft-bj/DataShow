using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Entity;
using Mysoft.Map.Extensions.DAL;

namespace My.Domain
{
    public  class DataShowDB
    {

       
        public List<DsEntityPage> GetPageList(Guid EntityPageGUID)
        {
            return CPQuery.From(
                "SELECT * FROM ds_EntityPage WHERE EntityPageGUID=@EntityPageGUID",
                new { EntityPageGUID= EntityPageGUID }
                ).ToList<DsEntityPage>();
        }

        public List<DsEntityPageNodeData> GetPageNodeList(Guid EntityPageNodeDataGUID) {
                return CPQuery.From(
                    "SELECT * FROM ds_EntityPageNodeData WHERE EntityPageNodeDataGUID=@EntityPageNodeDataGUID", 
                    new { EntityPageNodeDataGUID= EntityPageNodeDataGUID }
                    ).ToList<DsEntityPageNodeData>();
        }

    }
}

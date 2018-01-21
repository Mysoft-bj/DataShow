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
            return CPQuery.From("",new { }).ToList<DsEntityPage>();
        }

    }
}

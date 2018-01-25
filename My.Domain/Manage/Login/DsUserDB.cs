using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Entity;
using Mysoft.Map.Extensions.DAL;
using System.Data;

namespace My.Domain.Manage.Login
{
    public  class DsUserDB
    {

       
        public List<DsUser> GetCheckUserList(string UserCode,string PassWord)
        {
            return CPQuery.From(
                "SELECT id,UserName,UserCode,MobilePhone,IsAdmin from myuser where UserCode=@UserCode AND Password=@Password",
                new { UserCode = UserCode, Password = PassWord }
                ).ToList<DsUser>();
        }

        public List<DsEntityPageNodeData> GetPageNodeList(Guid EntityPageNodeDataGUID) {
                return CPQuery.From(
                    "SELECT * FROM ds_EntityPageNodeData WHERE EntityPageNodeDataGUID=@EntityPageNodeDataGUID", 
                    new { EntityPageNodeDataGUID= EntityPageNodeDataGUID }
                    ).ToList<DsEntityPageNodeData>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Domain;
using My.Core;
using System.Data;

namespace My.Application
{
    public  class UserService
    {
        public UserService(){
        }
        public static List<MyUser> GetUsers() {
            var sql = "SELECT UserName,UserCode,MobilePhone from myuser";
            DataTable userDt = MysqlDataBaseManager.GetDataBase().ExecuteDataTable(sql);
            List<MyUser> user = ConvertHelper<MyUser>.DataTableConvertToList(userDt);
            return user;
        }
    }
}

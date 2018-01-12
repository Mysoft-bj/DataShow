using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
    /// <summary>
    /// 数据库连接管理类
    /// </summary>
    public static class SqlDataBaseManager
    {
        #region 私有变量

       /// <summary>
       /// 连接字符串
       /// </summary>
        private static string ConnectionString;


        #endregion

        /// <summary>
        /// 创建数据访问类
        /// </summary>
        /// <returns>SQLServer数据库连接类</returns>
        public static SqlDataBase GetDataBase()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SQLConnString"].ToString();
            }
            return new SqlDataBase(ConnectionString);
        }
    }
}

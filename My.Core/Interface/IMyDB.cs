
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Core.Sql
{
    public interface IMyDB
    {
        int Execute(string sql, params  object[] args);
        T Scalar<T>(string sql, params  object[] args);
        int ScalarInt(string sql, params  object[] args);
        string ScalarString(string sql, params  object[] args);
        List<T> GetList<T>(string strSql, params  object[] args);
        T First<T>(string strSql, params  object[] args);
    }
    public class MyDB : IMyDB
    {
        
        public MyDB()
        {
         
        }
        public int Execute(string sql, params object[] args)
        {
            return IocManager.Resolve<IDatabase>() .Execute(sql, args);
        }

        public T Scalar<T>(string sql, params object[] args)
        {
            return IocManager.Resolve<IDatabase>().ExecuteScalar<T>(sql, args);
        }

        public int ScalarInt(string sql, params object[] args)
        {
            return IocManager.Resolve<IDatabase>().ExecuteScalar<int>(sql, args);
        }
        public string ScalarString(string sql, params object[] args)
        {
            return IocManager.Resolve<IDatabase>().ExecuteScalar<string>(sql, args);
        }

        public List<T> GetList<T>(string strSql, params object[] args)
        {
            return IocManager.Resolve<IDatabase>().Fetch<T>(strSql, args);
        }

        public T First<T>(string strSql, params object[] args)
        {
            return IocManager.Resolve<IDatabase>().FirstOrDefault<T>(strSql, args);
        }
    }



}

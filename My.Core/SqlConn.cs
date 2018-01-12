using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace My.Core{


    public class SqlConnect
    {
        public string ConnectionString { get; set; }
        /// <summary>
        /// 取值范围 Oracle.ManagedDataAccess.Client,System.Data.SqlClient,MySql.Data.MySqlClient
        /// </summary>
        public string ProviderName { get; set; }
        public string Name { get; set; }

        static ConcurrentDictionary<string, DbProviderFactory> _DbProviderFactoryDict
            = new ConcurrentDictionary<string, DbProviderFactory>();

        public DbProviderFactory GetDbProviderFactory()
        {
            var factory = _DbProviderFactoryDict.GetOrAdd(ProviderName, providerName =>
            {
                return DbProviderFactories.GetFactory(providerName);
            });
            return factory;
        }
    }

    public class SqlConnectProvider : ISingletonWatcher
    {

        public string WatcherFile
        {
            get { return "SqlConn.json"; }
        }
        public List<SqlConnect> ConnectionStrings
        {
            get;
            set;
        }
        /// <summary>
        /// 取值范围 Oracle.ManagedDataAccess.Client,System.Data.SqlClient,MySql.Data.MySqlClient
        /// </summary>
        public string DefaultProviderName { get; set; }
        public string MultiTenantHostName { get; set; }
        public  SqlConnect GetSqlConn(string name=null)
        {
            name = name ?? MultiTenantHostName;
            var sqlConn = ConnectionStrings.First(o => o.Name == name);
            sqlConn.ProviderName = sqlConn.ProviderName ?? DefaultProviderName;
            return sqlConn;
        }
    }
}

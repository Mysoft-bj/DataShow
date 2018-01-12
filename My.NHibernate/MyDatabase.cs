
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Data;
namespace My.NHibernate
{
    public class NhibernateDatabase :My.Core.Sql. Database
    {
        public ITransaction NhibernateTransaction{get;set;}
        public override System.Data.IDbCommand CreateCommand(IDbConnection connection,
            string sql, params object[] args)
        {
            var com = base.CreateCommand(connection, sql, args);
            if (NhibernateTransaction != null)
                NhibernateTransaction.Enlist(com);
            return com;
        }
      
        public NhibernateDatabase(IDbConnection connection,ITransaction transaction) : base(connection) {
            NhibernateTransaction = transaction;
        }

    }
}

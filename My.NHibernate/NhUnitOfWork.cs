using My.Core;
using My.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace My.NHibernate
{
    public class NhUnitOfWorkManager : IUnitOfWorkManager, IDisposable
    {
        public ISessionFactory  SessionFactory { get; private set; }
        ISession _session;
        public ISession Session { get{
            if (_session == null) {
                _session = SessionFactory.OpenSession();
            }
            return _session;
        } }
     
         public SqlConnect SqlConnect { get; set; }
        public IDbConnection DbConnection
        {
            get
            {
                return Session.Connection;
                
            }
        }

        public ITransaction DbTransaction { get; private set; }
     
        public NhUnitOfWorkManager(SqlConnect sqlConnect)
        {
            SessionFactory = NHibernateManager.GetSessionFactory(sqlConnect);
            SqlConnect = sqlConnect;
          

        }
        public IUnitOfWork Current { get; private set; }
        public IUnitOfWork Begin()
        {
            if (Current != null)
            {
                return new UnitOfWorkDependence();
            }
            
            DbTransaction = Session.BeginTransaction();
            Current = new NhUnitOfWork(Session);
            return Current;
        }
        public virtual void Dispose()
        {
            if (DbTransaction != null)
            {
                DbTransaction.Dispose();
                
            }
            if (DbConnection != null)
            {
                DbConnection.Dispose();

            }

        }
    }
    public class NhUnitOfWork : IUnitOfWork
    {
       
  
      public ISession Session { get; private set; }
      public NhUnitOfWork(ISession session)
        {

            Session = session;
        }
        public virtual void Complete()
        {
            if (Session != null && Session.Transaction != null) {
                try
                {

                    Session.Transaction.Commit();
                }
                catch
                {
                    Session.Transaction.Rollback();
                }
                finally
                {
                    Dispose();
                }
            }
           
        }

        public virtual void Dispose()
        {
           

        }
    }

    public class UnitOfWorkDependence : IUnitOfWork
    {

        public void Complete()
        {

        }
        public void Dispose()
        {

        }

    }
}

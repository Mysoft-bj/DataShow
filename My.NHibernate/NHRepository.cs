using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Domain;
using My.Core;
using System.Linq.Expressions;
using My.Core.Sql;
using System.Configuration;
using NHibernate.Linq;
using My.Core.Helper;
using NHibernate;
using My.Core.UnitOfWork;

namespace My.NHibernate
{

    public class NHRepository<T> : IRepository<T> where T : IEntity, new() 
    {

        NhUnitOfWorkManager UnitOfWorkManager { get;  set; }
        
        protected ISession Session
        {
            get {
                return UnitOfWorkManager.Session;
             
            }
           


        }

        public NHRepository(IUnitOfWorkManager unitOfWorkManager)
        {
            UnitOfWorkManager = (NhUnitOfWorkManager)unitOfWorkManager;
          
           // Session = nhcontext.Session;

        }

        public  IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            if (predicate==null)
            return Session.Query<T>();
            return Session.Query<T>().Where(predicate);
        }

        public virtual T Get(object id)
        {
            return Session.Get<T>(id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return Query(predicate).FirstOrDefault();
        }



        public virtual T Insert(T entity)
        {
            //   using (_context.)
            Session.Save(entity);
            Flush();
            return entity;
        }
        public void Flush()
        {
            if (Session.Transaction != null && Session.Transaction.IsActive)
                return;
            Session.Flush();
        }
        public virtual T InsertOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            Flush();
            return entity;
        }

        public virtual T Update(T entity)
        {
            Session.Update(entity);
            Flush();
            return entity;
        }


        public virtual T Delete(object id)
        {
            var obj = id as IEntity;
            if (obj != null)
                id = obj.GetValue("Id");
            var entity = Get(id);
            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                Update(entity);
            }
            else
            {
                Session.Delete(entity);
            }
            Flush();
            return entity;
        }
    }
}

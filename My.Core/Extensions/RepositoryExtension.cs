using My.Core.Sql;
using My.Core.Sql.Linq;
using My.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
   public static class RepositoryExtension
    {
       public static IDatabase GetDatabase<T>(this IRepository<T> repo) where T : IEntity
       {
           return IocManager.Resolve<IDatabase>();
       
       }
       public static IQueryable<T> Query<T>(this IRepository<T> repo) where T : IEntity
       {
           return repo.Query(null);
       }
       public static T Get<T>(this IRepository<T> repo, Expression<Func<T, bool>> expr) where T : IEntity
       {
           return repo.Query().FirstOrDefault(expr);
       }

       public static bool Exists<T>(this IRepository<T> repo, Expression<Func<T, bool>> expr) where T : IEntity
       {
           return repo.Query().Count(expr) > 0;
       }
    

       public static int DeleteMany<T>(this IRepository<T> repo, Expression<Func<T, bool>> whereExpression) where T : IEntity
       {
           var db= repo.GetDatabase();
           var ret = new DeleteQueryProvider<T>(db).Where(whereExpression).Execute();        
           return ret;
       }

       public static int UpdateMany<T>(this IRepository<T> repo, Expression<Func<T, bool>> whereExpression, Expression<Func<T, T>> updateExpression) where T : IEntity
       {
        
           var db = repo.GetDatabase();
      //     var ret = new UpdateQueryProvider<T>(db).Where(whereExpression).Execute(updateExpression);
           return 0;
       }
      
    }
   public class Repository : ISingletonDependency
   {
       public  IQueryable<T> Query<T>(Expression<Func<T, bool>> expr) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Query(expr);
       }
       public  IQueryable<T> Query<T>() where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Query();
       }

       public  IRepository<T> GetRepo<T>() where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>();
       }
       public  T GetById<T>(object entityId) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Get(entityId);
       }
       public  T Get<T>(Expression<Func<T, bool>> expr) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Query().FirstOrDefault(expr);
       }
       public  int DeleteMany<T>(Expression<Func<T, bool>> expr) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().DeleteMany(expr);
       }
       public  int UpdateMany<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExpression) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().UpdateMany(where, updateExpression);
       }

       public  bool Exists<T>(Expression<Func<T, bool>> expr) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Exists(expr);
       }
       public  T Delete<T>(T t) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Delete(t);
       }
       public  IEnumerable<T> GetList<T>(Expression<Func<T, bool>> expr) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Query(expr).ToList();
       }
       public  T Save<T>(T t) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().InsertOrUpdate(t);
       }
       public T Update<T>(T t) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Update(t);
       }
       public T Insert<T>(T t) where T : Entity
       {
           return IocManager.Resolve<IRepository<T>>().Insert(t);
       }

   }
}

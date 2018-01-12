using My.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
  //public static class Repo
  //  {
  //    public static IQueryable<T> Query<T>(Expression<Func<T, bool>> expr ) where T : IEntity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().Query(expr);
  //    }
  //    public static IQueryable<T> Query<T>() where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().Query();
  //    }

  //    public static IRepository<T> GetRepo<T>() where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>();
  //    }
  //    public static T GetById<T>(object entityId) where T : Entity {
  //        return IocManager.Resolve<IRepository<T>>().Get(entityId);
  //    }
  //    public static T Get<T>(Expression<Func<T, bool>> expr) where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().Query().FirstOrDefault(expr);
  //    }
  //    public static int DeleteMany<T>(Expression<Func<T, bool>> expr) where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().DeleteMany(expr);
  //    }
  //    public static int UpdateMany<T>(Expression<Func<T, bool>> where,Expression<Func<T, T>> updateExpression) where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().UpdateMany(where, updateExpression);
  //    }

  //    public static bool Exists<T>(Expression<Func<T, bool>> expr) where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().Exists(expr);
  //    }
  //    public static T Delete<T>(T t) where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().Delete(t);
  //    }
  //    public static IEnumerable<T> GetList<T>(Expression<Func<T, bool>> expr) where T : Entity
  //    {
  //        return IocManager.Resolve<IRepository<T>>().Query(expr).ToList();
  //    }

     
  //  }
}

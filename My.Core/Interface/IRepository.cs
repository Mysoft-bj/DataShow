using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using My.Core;
using My.Core.Sql;
using System.ComponentModel.DataAnnotations;
namespace My.Domain
{     
    public interface IRepository 
    {
        
    }
    public interface IService { }
    public interface IRepository<TEntity> : IRepository where TEntity : IEntity
    {         
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate );
        TEntity Get(object id);        
        TEntity Get(Expression<Func<TEntity, bool>> predicate); 
        TEntity Insert(TEntity entity);  
        TEntity InsertOrUpdate(TEntity entity);       
        TEntity Update(TEntity entity);
        TEntity Delete(object id);
     
    }

   

}


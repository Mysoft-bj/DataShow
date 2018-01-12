

namespace My.Core
{
    using System;
    using System.Linq.Expressions;   
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Check if this specification is satisfied by a 
        /// specific expression lambda
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> SatisfiedBy();
        /// <summary>
        /// 当前实体是否满足规约
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool IsSatisfiedBy(TEntity obj);
    }
}

namespace My.Core
{
    using System;
    using System.Linq.Expressions;
    public sealed class TrueSpecification<TEntity>
        :Specification<TEntity>
        where TEntity:class
    {
        #region Specification overrides

        
        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {           
            bool result = true;
            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }

        #endregion
    }
}

using My.Core.Sql.Expressions;

namespace My.Core.Sql.Linq
{
    public interface ISimpleQueryProviderExpression<TModel>
    {
        SqlExpression<TModel> AtlasSqlExpression { get; }
    }
}
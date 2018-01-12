using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq.Functions;
using System.Linq.Expressions;

using NHibernate.Linq;
using NHibernate.Hql.Ast;
using NHibernate.Hql.Ast.ANTLR.Tree;
using NHibernate.Hql.Ast.ANTLR;
using System.Reflection;
using NHibernate.Linq.Visitors;
using StringExtensions = My.Core.StringExtensions;
 namespace My.NHibernate

{
    public class StringGreaterEqualGenerator : BaseHqlGeneratorForMethod
    {
        public StringGreaterEqualGenerator()
        {
            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => StringExtensions.GreaterEqual(null, null)) };
        }
        public override HqlTreeNode BuildHql(System.Reflection.MethodInfo method, Expression targetObject, System.Collections.ObjectModel.ReadOnlyCollection<Expression> arguments,
           HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.GreaterThanOrEqual(
              visitor.Visit(arguments[0]).AsExpression(),
            visitor.Visit(arguments[1]).AsExpression());
        }


    }
    public class StringGreaterGenerator : BaseHqlGeneratorForMethod
    {
        public StringGreaterGenerator()
        {
            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => StringExtensions.Greater(null, null)) };
        }
        public override HqlTreeNode BuildHql(System.Reflection.MethodInfo method, Expression targetObject, System.Collections.ObjectModel.ReadOnlyCollection<Expression> arguments,
           HqlTreeBuilder treeBuilder,IHqlExpressionVisitor visitor)
        {
            return treeBuilder.GreaterThan(
              visitor.Visit(arguments[0]).AsExpression(),
            visitor.Visit(arguments[1]).AsExpression());
        }


    }
    public class StringLessEqualGenerator : BaseHqlGeneratorForMethod
    {
        public StringLessEqualGenerator()
        {
            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => StringExtensions.LessEqual(null, null)) };
        }
        public override HqlTreeNode BuildHql(System.Reflection.MethodInfo method, Expression targetObject, System.Collections.ObjectModel.ReadOnlyCollection<Expression> arguments,
           HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.LessThanOrEqual(
            visitor.Visit(arguments[0]).AsExpression(),
            visitor.Visit(arguments[1]).AsExpression());
        }


    }
    public class StringLessGenerator : BaseHqlGeneratorForMethod
    {
        public StringLessGenerator()
        {
            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => StringExtensions.Less(null, null)) };
        }
        public override HqlTreeNode BuildHql(System.Reflection.MethodInfo method, Expression targetObject, System.Collections.ObjectModel.ReadOnlyCollection<Expression> arguments,
           HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.LessThan(
            visitor.Visit(arguments[0]).AsExpression(),
            visitor.Visit(arguments[1]).AsExpression());
        }


    }
    public static class HqlTreeBuilderExtensions
    {
        public static HqlElements Elements(this HqlTreeBuilder treeBuilder, HqlExpression hashset)
        {
            var factory = (IASTFactory)treeBuilder.GetType().GetField("_factory", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(treeBuilder);

            return new HqlElements(factory, hashset);
        }
    }
    public class HqlElements : HqlExpression
    {
        public HqlElements(IASTFactory factory, HqlExpression hashset)
            : base(HqlSqlWalker.ELEMENTS, "elements", factory, hashset)
        {
            
        }
    }
    //public class HashsetSetGenerator : BaseHqlGeneratorForMethod 
    //{
      
    //    public HashsetSetGenerator()
    //    {
    //        SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() =>new HashSet<long>().Contains(0)) };
    //    }
    //    public override HqlTreeNode BuildHql(System.Reflection.MethodInfo method, Expression targetObject, System.Collections.ObjectModel.ReadOnlyCollection<Expression> arguments,
    //       HqlTreeBuilder treeBuilder, NHibernate.Linq.Visitors.IHqlExpressionVisitor visitor)
    //    {
    //        var value = visitor.Visit(arguments[1]).AsExpression();
    //        HqlTreeNode inClauseNode;

    //        if (arguments[1] is ConstantExpression)
    //            inClauseNode = BuildFromArray((Array)((ConstantExpression)arguments[0]).Value, treeBuilder);
    //        else
    //            inClauseNode = BuildFromExpression(arguments[0], visitor);

    //        HqlTreeNode inClause = treeBuilder.In(value, inClauseNode);

    //        if (method.Name == "NotIn")
    //            inClause = treeBuilder.BooleanNot((HqlBooleanExpression)inClause);

    //        return inClause;
    //    }
    //    private HqlTreeNode BuildFromExpression(Expression expression,
    //            IHqlExpressionVisitor visitor)
    //    {
    //        //TODO: check if it's a valid expression for in clause, 
    //        //i.e. it selects only one column
    //        return visitor.Visit(expression).AsExpression();
    //    }

    //    private HqlTreeNode BuildFromArray(Array valueArray, HqlTreeBuilder treeBuilder)
    //    {
    //        var elementType = valueArray.GetType().GetElementType();

    //        if (!elementType.IsValueType && elementType != typeof(string))
    //            throw new ArgumentException("Only primitives and strings can be used");

    //        Type enumUnderlyingType = elementType.IsEnum ?
    //        Enum.GetUnderlyingType(elementType) : null;
    //        var variants = new HqlExpression[valueArray.Length];

    //        for (int index = 0; index < valueArray.Length; index++)
    //        {
    //            var variant = valueArray.GetValue(index);
    //            var val = variant;

    //            if (elementType.IsEnum)
    //                val = Convert.ChangeType(variant, enumUnderlyingType);

    //            variants[index] = treeBuilder.Constant(val);
    //        }

    //        return treeBuilder.DistinctHolder(variants);
    //    }

    //}


    public class MyLinqToHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public MyLinqToHqlGeneratorsRegistry()
        {
            RegisterGenerator(ReflectionHelper.GetMethodDefinition(
                () => StringExtensions.GreaterEqual(null, null)), new StringGreaterEqualGenerator());

            RegisterGenerator(ReflectionHelper.GetMethodDefinition(
              () => StringExtensions.LessEqual(null, null)), new StringLessEqualGenerator());

            RegisterGenerator(ReflectionHelper.GetMethodDefinition(
               () => StringExtensions.Greater(null, null)), new StringGreaterGenerator());

            RegisterGenerator(ReflectionHelper.GetMethodDefinition(
              () => StringExtensions.Less(null, null)), new StringLessGenerator());



         //   RegisterGenerator(ReflectionHelper.GetMethodDefinition(() => new HashSet<long>().Contains( 0)), new HashsetSetGenerator());
        }
    }
}

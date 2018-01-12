using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace My.Application
{
    public static class ValidationExtensions
    {
       
    }
    public interface IValidation
    {
        Validator Validate();
    }
    public interface IValidationRule{
    
    }
    public class Rule
    {
        public static RequireRule Require()
        {
            return new RequireRule();

        }
    }

    public class RequireRule : IValidationRule { 
    
    }
    public class Validator
    {
        public Dictionary<System.Reflection.PropertyInfo, IValidationRule> ValidationRules
          = new Dictionary<System.Reflection.PropertyInfo, IValidationRule>();


    }
    public class Validator<T>
    {

        public Validator<T> AddRule(Expression<Func< T, object>> prop, IValidationRule rule)
        {
            return this;
        }


    }
}

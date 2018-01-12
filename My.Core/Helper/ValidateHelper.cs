using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace My.Core
{


    public static class ValidateHelper
    {
        public static void ValidateEntity(object entity)
        {
            var meta = MetaDataHelper.GetMetaData(entity.GetType());
            foreach (var prop in meta.Properties.Values)
            {
                var val = prop.GetValue(entity);
                if (prop.RequiredAttribute!=null)
                {
                    if (val == null) throw new ValidationException(prop.DisplayName + "字段必填");
                    if (prop.PropertyInfo.PropertyType == typeof(string) && val.ToString().IsNullOrWhiteSpace())
                    {
                        var errMsg = prop.RequiredAttribute.ErrorMessage ?? prop.DisplayName + "字段必填";
                        throw new ValidationException(errMsg);
                    }
                      
                }
                if (prop.StringLengthAttribute !=null)
                {
                    if (val != null && val.ToString().Length > prop.StringLengthAttribute.MaximumLength)
                    {
                        var errMsg = prop.StringLengthAttribute.ErrorMessage ?? prop.DisplayName + "字段长度不能超过" + prop.StringLengthAttribute;
                        throw new ValidationException(errMsg);
                    }
                        
                }
            }
        }

    }
}

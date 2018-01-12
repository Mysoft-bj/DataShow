using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using My.Domain;
namespace My.Core
{

    
    public static class MyEnumHelper
    {
        public class EnumEntity  {
            public string Text { get; set; }
            public int Value { get; set; }
            public string Name { get; set; }
        }

        static ConcurrentDictionary<Type, IEnumerable<EnumEntity>> enumDic = new ConcurrentDictionary<Type, IEnumerable<EnumEntity>>();
        public static string GetText(object val) 
        {
            Type enumType = val.GetType();
            var list = enumDic.GetOrAdd(enumType, GetList);
            return list.FirstOrDefault(o => o.Value == (int)val).Text;
        }
      
        public static IEnumerable<EnumEntity> GetList(Type enumType)
        {
            if (enumType.IsGenericType && enumType.GetGenericTypeDefinition() == typeof(Nullable<>))
                enumType = Nullable.GetUnderlyingType(enumType);
            var list = new List<EnumEntity>();
            FieldInfo[] fields = enumType.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (!field.FieldType.IsEnum)
                    continue; ;
                var disply = field.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();
                var enumText = field.GetValue(null).ToString();
                int key = Convert.ToInt32(Enum.Parse(enumType, enumText, true));
                if (disply != null)
                {
                    enumText = disply.Name;
                }
                var entity = new EnumEntity() { Value = key, Name = field.Name, Text = enumText };
                list.Add(entity);
            }
            return list;
        }
     
       
    }
}

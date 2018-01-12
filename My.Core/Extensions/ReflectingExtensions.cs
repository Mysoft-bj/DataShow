using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Collections.Specialized;
using System.Collections.Concurrent;
namespace My.Core
{
    public static class MyReflectionHelper
    {
        static ConcurrentDictionary<string, Func<object, object[], object>> _cacheDelegate = new ConcurrentDictionary<string, Func<object, object[], object>>();
        public static object InvokeMethod(Type type, string method, Type[] types, object instance = null, object[] param = null)
        {
            string key=  type.Name + "." + method + types[0].Name;
           
            if (types.Length > 1)
            {
                key += types[1].Name;
            }
            var del = _cacheDelegate.GetOrAdd(key, (k) =>
            {
               var mis= type.GetMethods().Where(m => m.IsGenericMethod && m.Name == method);
               foreach (var mi in mis)
                {
                    try
                    {
                        MethodInfo gmi = mi.MakeGenericMethod(types);
                        return ReflectingExtensions.ComplieMethod(gmi);
                    }
                    catch { }
                }
               throw new Exception("找不到合适的泛型方法!" + type.FullName + " " + method);
              //  MethodInfo mi = type.GetMethod(method, types);
             
                //  gmi.Invoke(instance, param);
               
            });
            if (param == null)
                param = new object[] { };
            return del(instance, param);

        }

      
        public static object InvokeProperty(Type type, string propertyName, params Type[] types)
        {
            return InvokeMethod(type, "get_" + propertyName, types, null, new object[] { });

        }

        public static object InvokeMethod(object inst, string method, NameValueCollection param)
        {
            var type = inst.GetType();
            var mis = type.GetMethods().Where(m => !m.IsGenericMethod && m.Name.ToLower() == method.ToLower()).FirstOrDefault();
            var ps = mis.GetParameters();
            var pval = new object[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                var pp = ps[i];
                var val = param[pp.Name];
                var oval = Convert.ChangeType(val, pp.ParameterType);
                pval[i] = oval;
                mis.Invoke(inst, pval);

            }
            return null;

        }


    }
    public static class ReflectingExtensions
    {
        static Dictionary<string, Action<object, object>> _cacheSetMethod = new Dictionary<string, Action<object, object>>(StringComparer.OrdinalIgnoreCase);
        static Dictionary<string, Func<object, object>> _cacheGetMethod = new Dictionary<string, Func<object, object>>(StringComparer.OrdinalIgnoreCase);
        public static void SetValue<T>(this T obj, string propertyname, object value)
        {
            var type = obj.GetType();
            var cacheKey = type.FullName + "." + propertyname;
            Action<object, object> setMethod;
            if (!_cacheSetMethod.TryGetValue(cacheKey, out setMethod))
            {
                var property = obj.GetType().GetProperty(propertyname, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);
                if (property == null)
                    throw new Exception("无法找到类型的属性：" + cacheKey);
                setMethod = property.ComplieSetMethod();
                if (setMethod == null)
                    throw new Exception("类型的属性不可设置值：" + cacheKey);
                _cacheSetMethod[cacheKey] = setMethod;
            }
            setMethod(obj, value);
        }
        public static object GetValue<T>(this T obj, string propertyname)
        {
            var type = obj.GetType();
            var cacheKey = type.FullName + "." + propertyname;
            Func<object, object> getMethod;
            if (!_cacheGetMethod.TryGetValue(cacheKey, out getMethod))
            {
                var property = obj.GetType().GetProperty(propertyname, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);
                if (property == null)
                    throw new Exception("无法找到类型的属性：" + cacheKey);
                getMethod = property.ComplieGetMethod();
                if (getMethod == null)
                    throw new Exception("类型的属性不可读取值：" + cacheKey);
                _cacheGetMethod[cacheKey] = getMethod;
            }
            return getMethod(obj);
        }

        public static T GetCustAttr<T>(this MemberInfo mi, bool inherit = false) where T : class
        {
            object[] arr = mi.GetCustomAttributes(typeof(T), true);
            if (arr.Length > 0)
                return (T)arr[0];
            return default(T);
        }
        public static Func<T, object> ComplieFunction<T>(this PropertyInfo property)
        {
            if (!property.CanRead) return null;
            // Target: (object)(((TInstance)instance).Property)
            // preparing parameter, object type
            var instance = Expression.Parameter(typeof(T), "instance");

            var propertyAccess = Expression.Property(instance, property);
            // (object)(((TInstance)instance).Property)
            var castPropertyValue = Expression.Convert(propertyAccess, typeof(object));
            // Lambda expression
            var lambda = Expression.Lambda<Func<T, object>>(castPropertyValue, instance);
            return lambda.Compile();
        }
        public static Func<T, object> ComplieFunction<T>(this PropertyDescriptor property)
        {
            return property.ComponentType.GetProperty(property.Name).ComplieFunction<T>();

        }

        public static Func<object, object> ComplieGetMethod(this PropertyInfo property)
        {
            if (!property.CanRead) return null;
            var instance = Expression.Parameter(typeof(object), "instance");
            var instanceCast = property.GetGetMethod(true).IsStatic ? null :
                Expression.Convert(instance, property.ReflectedType);
            var propertyAccess = Expression.Property(instanceCast, property);
            var castPropertyValue = Expression.Convert(propertyAccess, typeof(object));
            var lambda = Expression.Lambda<Func<object, object>>(castPropertyValue, instance);
            return lambda.Compile();
        }
        public static Func<object, object[], object> ComplieMethod(MethodInfo methodInfo)
        {
            // Target: ((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)

            // parameters to execute
            var instanceParameter = Expression.Parameter(typeof(object), "instance");
            var parametersParameter = Expression.Parameter(typeof(object[]), "parameters");

            // build parameter list
            var parameterExpressions = new List<Expression>();
            var paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                // (Ti)parameters[i]
                BinaryExpression valueObj = Expression.ArrayIndex(
                    parametersParameter, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(
                    valueObj, paramInfos[i].ParameterType);

                parameterExpressions.Add(valueCast);
            }

            // non-instance for static method, or ((TInstance)instance)
            var instanceCast = methodInfo.IsStatic ? null :
                Expression.Convert(instanceParameter, methodInfo.ReflectedType);

            // static invoke or ((TInstance)instance).Method
            var methodCall = Expression.Call(instanceCast, methodInfo, parameterExpressions);

            // ((TInstance)instance).Method((T0)parameters[0], (T1)parameters[1], ...)
            if (methodCall.Type == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object, object[]>>(
                        methodCall, instanceParameter, parametersParameter);

                Action<object, object[]> execute = lambda.Compile();
                return (instance, parameters) =>
                {
                    execute(instance, parameters);
                    return null;
                };
            }
            else
            {
                var castMethodCall = Expression.Convert(methodCall, typeof(object));
                var lambda = Expression.Lambda<Func<object, object[], object>>(
                    castMethodCall, instanceParameter, parametersParameter);

                return lambda.Compile();
            }
        }
        public static Action<object, object> ComplieSetMethod(this PropertyInfo property)
        {
            if (!property.CanWrite) return null;
            var methodInfo = property.GetSetMethod(true);
            var instance = Expression.Parameter(typeof(object));
            var value = Expression.Parameter(typeof(object));

            var castValue = Expression.Convert(value, property.PropertyType);
            var instanceCast = Expression.Convert(instance, property.ReflectedType);
            var setPropertyValue = Expression.Call(instanceCast, property.GetSetMethod(true), castValue);
            return Expression.Lambda<Action<object, object>>
                (setPropertyValue, instance, value).Compile();
        }
    }
}

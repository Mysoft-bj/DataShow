using System;
using System.Reflection;


namespace My.Core
{
   
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UnitOfWorkAttribute : Attribute
    {
    }
}
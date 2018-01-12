using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace My.Core
{
   public class AssemblyHelper
    {
       
       public List<Assembly> GetAllAssemblies()
       {
           return AppDomain.CurrentDomain.GetAssemblies().ToList();
       }
       private List<Type> GetAllTypes()
       {
           var allTypes = new List<Type>();

           foreach (var assembly in GetAllAssemblies().Distinct())
           {
               try
               {
                   Type[] typesInThisAssembly;

                   try
                   {
                       typesInThisAssembly = assembly.GetTypes();
                   }
                   catch (ReflectionTypeLoadException ex)
                   {
                       typesInThisAssembly = ex.Types;
                   }

                   if (typesInThisAssembly.IsNullOrEmpty())
                   {
                       continue;
                   }

                   allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
               }
               catch (Exception ex)
               {
                   Logger.Error(ex);
               }
           }

           return allTypes;
       }
       public Type[] Find(Func<Type, bool> predicate)
       {
           return GetAllTypes().Where(predicate).ToArray();
       }
    }
}

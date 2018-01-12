using System;
using System.Collections.Generic;
using System.Linq;

using My.Core;
using System.Reflection;
using System.Web;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using My.Domain;
using FluentNHibernate.Mapping;
namespace My.NHibernate

{
   
    public class NFMapGenerate 
    {
        public static List<Type> GetEntityTypes() {
            return null;
            //return Assembly.GetAssembly(typeof(Account)).GetTypes()
            //        .Where(x => typeof(IEntity).IsAssignableFrom(x) && !x.IsAbstract).ToList();
        }
        public static Assembly GenerateAssembly(Assembly entityAssem,out List<Type> cacheTypes)
        {
            cacheTypes = new List<Type>();
            //创建编译器实例。   
          var  provider = new CSharpCodeProvider();
            //设置编译参数。   
          var  paras = new CompilerParameters();
             var entityAssemName = entityAssem.FullName.Split(',')[0];
            paras.GenerateExecutable = false;
            paras.GenerateInMemory = true;          
            StringBuilder sbAssembly = new StringBuilder();
            sbAssembly.AppendLine("using System ;");
            sbAssembly.AppendLine("using FluentNHibernate.Mapping; ");
            sbAssembly.AppendLine("using System.Linq;");
            sbAssembly.AppendLine("using System.Linq.Expressions;");          
            sbAssembly.AppendLine("namespace Dynamic." + entityAssemName.Replace(".","") + ".mapping { ");
            foreach (var type in entityAssem.GetTypes())
            {
                if (typeof(Entity).IsAssignableFrom(type) && type.IsClass)
                {


                    sbAssembly.Append(GenerateClass(type));
                    //var cache = type.GetCustAttr<CacheAttribute>();
                    //if (cache != null)
                    //    cacheTypes.Add(type);
                }
            }
            sbAssembly.AppendLine("}");      
            var assemblies = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .Where(a => !a.IsDynamic)
                            .Select(a => a.Location);
            foreach (var refName in assemblies)
            {
                paras.ReferencedAssemblies.Add(refName);
            }
            CompilerResults result = provider.CompileAssemblyFromSource(paras, sbAssembly.ToString());
            string path = Path.Combine(PathHelper.AppDataPath, "hbmXml");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, entityAssemName + "Map.cs"), false, Encoding.UTF8))
            {
                sw.Write(sbAssembly.ToString());

            }
            //获取编译后的程序集。   
            Assembly assembly = result.CompiledAssembly;

            return assembly;
          
        }
         static HashSet<string> indexNameSet = new HashSet<string>();
      public  static string GenerateClass(Type type)
        {           
            StringBuilder sb = new StringBuilder();
            var meta = MetaDataHelper.GetMetaData(type);
            if (meta.TableName.IsNullOrWhiteSpace() || meta.KeyColumn == null)
                throw new Exception(type.FullName + ":没有配置主键或者表名");
            sb.Append("public   class   ").Append(type.Name).Append("Map : ClassMap<").Append(type.FullName).Append(">  \n");
            sb.AppendLine("{");
            var tableName = meta.TableName;
            sb.Append("public ").Append(type.Name).AppendLine("Map (){ ");
            //bool cache=type.GetCustAttr<CacheAttribute>()!=null;
            //if (cache)
            //sb.AppendLine("this.Cache.ReadWrite();");
            sb.Append("Table(\"").Append(tableName).AppendLine("\"); ");

          
            foreach (var prop in meta.Properties.Values)
            {
                if (prop.IsNotMapped)
                {
                    continue;
                }     
                string linq = "(m=>m." + prop.Name + ")";
                if (prop.HasManyAttribute != null)
                {
                    sb.Append("HasMany").Append(linq);
                    //if (TypeHelper.GetElementType(prop.PropertyDescriptor.PropertyType).IsValueType)
                    //    sb.AppendFormat(".Element(\"{0}\").KeyColumn(\"{1}\").Table(\"{2}\")", hasMany.ChildKey, hasMany.ParentKey, hasMany.Table);
                    //else
                    sb.Append(".KeyColumn(\"" + prop.HasManyAttribute.ParentKey + "\")");
                    if (prop.HasManyAttribute.OrderBy.IsNotNull())
                    {
                        sb.AppendFormat(".OrderBy(\"{0}\")", prop.HasManyAttribute.OrderBy);
                    }
                    //else if (typeof(ISortable).IsAssignableFrom(prop.HasManyAttribute.ItemType))
                    //{

                    //    sb.AppendFormat(".OrderBy(\"Sort\")");
                    //}

                  
                    //if (cache)
                    //    sb.Append(".Cache.ReadWrite()");
                    sb.Append(";\n");
                    continue;
                }
             
                if (prop.HasOneAttribute != null)
                {
                    sb.Append("References").Append(linq).Append(".Column(\"" + prop.HasOneAttribute.ReferenceID + "\")");
                    //if (cache)
                    //    sb.Append(".Cache.ReadWrite()");
                    sb.Append(";\n");
                    continue;
                }
                var manyToMany = prop.HasMany2ManyAttribute;
                if (manyToMany != null)
                {
                    sb.Append("HasManyToMany").Append(linq);
                //    if (manyToMany.OrderBy.IsNotNull())
                //    sb.AppendFormat(".AsMap(\"{0}\")", manyToMany.OrderBy);

                    string temp = ".Table(\"{0}\").ParentKeyColumn(\"{1}\").ChildKeyColumn(\"{2}\")";
                  
                    sb. Append(string.Format(temp, manyToMany.MidTable, manyToMany.ParentKey, manyToMany.ChildKey));
                    if (manyToMany.OrderBy.IsNotNull())
                    {
                       sb.AppendFormat(".OrderBy(\"{0}\")", manyToMany.OrderBy);
                    }
                    //else if (typeof(ISortable).IsAssignableFrom(manyToMany.ItemType))
                    //{

                    //    sb.AppendFormat(".OrderBy(\"Sort\")");
                    //}
                    //if (cache)
                    //    sb.Append(".Cache.ReadWrite()");
                    sb.Append(";\n");
                    continue;
                }
              
               
                if (prop.IsKeyColumn)
                {
                     sb.Append("Id").Append(linq).Append(".GeneratedBy");
                 //   GuidComb Assigned
                     if (prop.PropertyDescriptor.PropertyType == typeof(Guid))
                         sb.Append(".GuidComb()");
                     else
                         sb.Append(".Assigned()");

                }
                else
                {
                    sb.Append("Map").Append(linq);
                }
                if (prop.RequiredAttribute!=null)
                {
                    sb.Append(".Not.Nullable()");
                }

                if (prop.PropertyDescriptor.PropertyType == typeof(string))
                {
                    var maxLen = 0;
                    if (prop.StringLengthAttribute != null)
                        maxLen = prop.StringLengthAttribute.MaximumLength;
                    if (maxLen == 0)
                        maxLen = 255;
                    sb.AppendFormat(".Length({0})", maxLen);
                }
                if (prop.IndexAttribute != null)
                {
                    var indexName = "ix_" + meta.TableName + "_" + prop.Name;
                    if (!indexNameSet.Contains(indexName))
                    {
                        indexNameSet.Add(indexName);
                        if (prop.IndexAttribute.IsUnique)
                        {
                            sb.AppendFormat(".UniqueKey(\"{0}\")", indexName);
                        }
                        else
                        {
                            sb.AppendFormat(".Index(\"{0}\")", indexName);
                        }
                    }
                }
                if (prop.ColumnName != prop.Name) {
                    sb.AppendFormat(".Column(\"{0}\")", prop.ColumnName);
                }
                 sb.AppendLine(";");
            }
            sb.AppendLine("}} ");
         //   sb.Append(@"public class usermap { public string userid {get;set;} }");
           return sb.ToString();
        }
    }
    public class TestEntity {
        public string Id { get; set; }
    }
    public class TestEntityMap : ClassMap<TestEntity> {
        public TestEntityMap() { 
       // Id(o=>o.Id).GeneratedBy.GuidComb().Column
        }
    }
}
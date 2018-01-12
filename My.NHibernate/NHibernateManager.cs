#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;
using My.Core;
using System.Web;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.AdoNet;
using NHibernate.AdoNet.Util;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Exceptions;
using NHibernate.Connection;
using System.IO;
using System.Xml;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using System.Reflection;
using NHibernate.Cache;
using System.Threading;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;

using System.Collections.Concurrent;
#endregion
namespace My.NHibernate
{   
    public static class NHibernateManager
    {
      static  Dictionary<string, ISessionFactory> _cacheSessionFactory = new Dictionary<string, ISessionFactory>();
      static object _lockObj = new object();
      static void SetDefaultConfig(FluentConfiguration config)
      {
           
            config.Mappings(c =>
            {

                c.FluentMappings.Conventions.AddFromAssemblyOf<EnumConvention>();
                var hbmXmlPath = Path.Combine(PathHelper.AppDataPath, "hbmXml");
                DirectoryHelper.CreateIfNotExists(hbmXmlPath);
                c.FluentMappings.AddFromAssembly(_entityMapAssembly).ExportTo(hbmXmlPath);
                c.FluentMappings.Conventions.Add(DefaultLazy.Never());
                c.FluentMappings.Conventions.Add(
             ConventionBuilder.HasMany.Always(x =>
             {
                 x.Fetch.Join();


             }),
             ConventionBuilder.HasManyToMany.Always(x =>
             {
                 x.Fetch.Join();

             }),
               ConventionBuilder.Reference.Always(x =>
               {
                   x.ReadOnly();
                   x.Fetch.Join();
                   x.Nullable();
                   x.NotFound.Ignore();
               }));

            });
           
            config.Cache(x => x.ProviderClass<NoCacheProvider>());
        }
        static NHibernateManager() {
            BuildDymicAssembly();
        
        }
        public static ISessionFactory GetSessionFactory(SqlConnect sqlConnectProvider)
        {
            ISessionFactory factory = null;
            if (_cacheSessionFactory.TryGetValue(sqlConnectProvider.Name, out factory))
                return factory;
            lock (_lockObj) {
                if (_cacheSessionFactory.TryGetValue(sqlConnectProvider.Name, out factory))
                    return factory;
                FluentConfiguration config = Fluently.Configure();
                SetDefaultConfig(config);
                // if (conn.ProviderName.IndexOf("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) > -1)
                BuildMsSqlDatabase(config, sqlConnectProvider.ConnectionString);
                config.ExposeConfiguration(cfg =>
                {

                    cfg.SetProperty("command_timeout", "120");
                    var export = new SchemaExport(cfg).SetOutputFile(Path.Combine(PathHelper.AppDataPath, "myDDL.sql"));
                    export.Create(true, false);
                });
                factory= config.BuildSessionFactory();
                _cacheSessionFactory[sqlConnectProvider.Name]=factory;

            }

            return factory;


        }

        static FluentConfiguration BuildOracleDatabase(FluentConfiguration config, string connectString)
        {
            config.Database(OracleDataClientConfiguration.Oracle10
                     .ConnectionString(connectString)
      .Driver<OracleManagedDataClientDriver>().Provider<DriverConnectionProvider>()
      .Dialect<Oracle10gDialect>()     
      .ShowSql().AdoNetBatchSize(50));
            return config;
        }

        static FluentConfiguration BuildMsSqlDatabase(FluentConfiguration config, string connectString)
        {
            config.Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectString)
               .AdoNetBatchSize(50)
               .IsolationLevel(IsolationLevel.ReadUncommitted)
               .ShowSql()               
               .FormatSql()       
               );
             
            return config;
        }
        static Assembly _entityMapAssembly;

        static void BuildDymicAssembly()
        {
            
            var ddlPath = Path.Combine(PathHelper.BinPath, "My.Domain.dll");
            Assembly entityAssembly = Assembly.LoadFrom(ddlPath);
            List<Type> cacheTypes;
             _entityMapAssembly = NFMapGenerate.GenerateAssembly(entityAssembly, out cacheTypes);
        }
       
            
          
             //   m.FluentMappings.Conventions.Add(
                    //   ConventionBuilder.Id.Always(x => x.GeneratedBy.Identity()),
                     //ConventionBuilder.HasMany.Always(x =>
                     //{
                     //    x.Fetch.Join();
                     //    x.ReadOnly();
                     //    x.Cascade.None();
                     //}),
                     //ConventionBuilder.HasManyToMany.Always(x =>
                     //{
                     //    x.Fetch.Join();
                     //    x.ReadOnly();

                     //    x.Cascade.None();

                     //}),
                     //  ConventionBuilder.Reference.Always(x =>
                     //  {
                     //      x.ReadOnly(); x.Fetch.Join();
                     //      x.Nullable(); x.NotFound.Ignore(); x.Cascade.None();
                     //  }),
                     //DefaultCascade.None(),
                     //DefaultLazy.Never());


           
           
          
         //   SessionFactory = _config.BuildSessionFactory();
            //   cacheTypes.ForEach(t => cfg.CacheEntity(t));
       

        
        //public void ExportSchema()
        //{
        //    _config.ExposeConfiguration(cfg =>
        //    {
        //        new SchemaExport(cfg).SetOutputFile(Path.Combine(PathHelper.AppDataPath, "myDDL.sql")).Create(true, true);
        //    });

        //    _config.BuildConfiguration();
        //    SessionFactory = _config.BuildSessionFactory();

        //    //update.Execute(true, false);


        //}
        //public static void BatchUpdate<T>(IEnumerable<T> entities)
        //{
        //    using (var s = IocManager.Resolve<NHibernateManager>().SessionFactory.OpenStatelessSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (var entity in entities)
        //                {
        //                    s.Update(entity);
        //                }
        //                tx.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                tx.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}
        //public static void BatchInsert<T>(IEnumerable<T> entities)
        //{
        //    using (var s = IocManager.Resolve<NHibernateManager>().SessionFactory.OpenStatelessSession())
        //    {
        //        using (var tx = s.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (var entity in entities)
        //                {
        //                    s.Insert(entity);
        //                }
        //                tx.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                tx.Rollback();
        //                throw ex;
        //            }
        //        }
        //    }
        //}
    }

}
 
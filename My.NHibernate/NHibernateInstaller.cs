using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using My.Core;
using My.Domain;
using System.Reflection;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using NHibernate;
using My.Core.Sql;
using My.NHibernate.Interceptors;
using My.Core.UnitOfWork;
namespace My.NHibernate
{
    public class NHibernateInstaller : DefaultInstaller
    {
        public override void Register(IWindsorContainer container, IConfigurationStore store)
        {
           
            container.Register( Component.For<NHibernateInterceptor>()
                .ImplementedBy<NHibernateInterceptor>().LifestyleTransient());                    
            container.Register(Component.For(typeof(IUnitOfWorkManager))
                .ImplementedBy(typeof(NhUnitOfWorkManager)).LifestylePerWebRequest());
            container.Register(Component.For(typeof(IMyDB)).
                ImplementedBy(typeof(MyDB)).LifestylePerWebRequest());

            container.Register(Component.For<SqlConnect>()
                .UsingFactoryMethod((ioc) =>
                {
                    string tenant = null;
                    var sqlConnectProvider=IocManager.Resolve<SqlConnectProvider>();
                    tenant = tenant ?? sqlConnectProvider.MultiTenantHostName;
                   return sqlConnectProvider.GetSqlConn(tenant);
                  
                }).LifestylePerWebRequest());
            //
           
          
         
            container.Register(Component.For<IDatabase>().
                UsingFactoryMethod((ioc) =>
                {
                    var UnitOfWorkManager = (NhUnitOfWorkManager)ioc.Resolve<IUnitOfWorkManager>();
                    if (UnitOfWorkManager.Current != null) {
                        if (UnitOfWorkManager.Session.IsDirty())
                            UnitOfWorkManager.Session.Flush();                       
                    }
                    var db = new NhibernateDatabase(UnitOfWorkManager.DbConnection, UnitOfWorkManager.DbTransaction);
                    return db;     
             
                }).LifestyleTransient());


            container.Register(
               Component.For(typeof(IRepository<>))
               .ImplementedBy(typeof(NHRepository<>)).LifestyleTransient()
               );

            //var entityTypes = Assembly.GetAssembly(typeof(User)).GetTypes()
            //   .Where(x => typeof(IEntity).IsAssignableFrom(x) && !x.IsAbstract).ToList();
            //foreach (var entityType in entityTypes)
            //{
            //    var genericRepositoryType = typeof(IRepository<>).MakeGenericType(entityType);
            //    if (!container.Kernel.HasComponent(genericRepositoryType))
            //    {

            //        container.Register(
            //            Component.For(genericRepositoryType)
            //            .ImplementedBy(typeof(NHRepository<>).MakeGenericType(entityType))
            //            .LifestyleTransient());

            //    }
            //}

            base.Register(container, store);
        }

        
    
     
    }
}

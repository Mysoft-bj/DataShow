using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Core;
using My.Core.UnitOfWork;
using Castle.MicroKernel;
using My.Domain;
using System.Linq;
namespace My.Core
{
    
    public class IocManager 
    {
        
        public static IocManager Instance { get; private set; }

        
        public static IWindsorContainer IocContainer { get; private set; }

       

        static IocManager()
        {
            Instance = new IocManager();
            IocContainer = new WindsorContainer();
          IocContainer.Kernel.ComponentRegistered += ComponentRegistered;;
        }
        private static void ComponentRegistered(string key, IHandler handler)
        {
            //if (typeof(IService).IsAssignableFrom(handler.ComponentModel.Implementation))
            //{
            //    //Intercept all methods of all repositories.
            //    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            //}
            //else 
            //if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(m=> m.IsDefined(typeof(UnitOfWorkAttribute), true)))
            //{
            //    //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
            //    //TODO: Intecept only UnitOfWork methods, not other methods!
            //    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            //}
        }
        public static bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

       
        public static bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        
        public static T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

       
        public static T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

      
        public static object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        
        public static object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

      
        public static void Release(object obj)
        {
            IocContainer.Release(obj);
        }

     

        
    }
}
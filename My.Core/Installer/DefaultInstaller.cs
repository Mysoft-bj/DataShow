using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.LifecycleConcerns;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel;
using StackExchange.Redis;
using My.Domain;
using System.Reflection;
using Castle.Core;
using My.Core.UnitOfWork;
using System.Linq;
namespace My.Core
{
   
    public class DefaultInstaller : IWindsorInstaller 
    {
      public  Assembly CurrentAssembly{get; set;}
        public DefaultInstaller() {
            CurrentAssembly = this.GetType().Assembly;
        }
        public virtual void Register(IWindsorContainer container, IConfigurationStore store)
        {
            AppAuthorizeManager.Authorize();
            
        }

    
        public virtual void ComponentRegistered(string key, IHandler handler)
        {
            var type=handler.ComponentModel.Implementation;
            //typeof(IRepository).IsAssignableFrom(type) ||
            if  ( typeof(IService).IsAssignableFrom(type))
            {
              
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | 
                BindingFlags.NonPublic).Any(m=>m.IsDefined(typeof(UnitOfWorkAttribute), true)))
            {
               
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
      
        public LifecycleActionDelegate<object> watcherOnCreate= (kernel, inst) => {
            ISingletonWatcher singInst = inst as ISingletonWatcher;
            if (singInst == null)
                return;
            Watcher.StartWatch(singInst);
        };
        public void RegisterInterfaces<T>() {
           
            IocManager.IocContainer.Register(
                  Classes.FromAssembly(CurrentAssembly)
                      .IncludeNonPublicTypes()
                      .BasedOn<T>()
                      .WithService.Self()
                      .WithService.DefaultInterfaces()
                      .LifestyleTransient()
                  );
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += ComponentRegistered;
            //Transient
           container.Register(
                Classes.FromAssembly(CurrentAssembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );
           //Transient
          
            //SingletonWithFile
            IocManager.IocContainer.Register(
             Classes.FromAssembly(CurrentAssembly)
                    .BasedOn<ISingletonWatcher>()
                    .WithService.Self()
                    .WithServiceSelf()
                    .Configure(c => c.OnCreate(watcherOnCreate))
                    .LifestyleSingleton()
                );

            //Singleton
            IocManager.IocContainer.Register(
             Classes.FromAssembly(CurrentAssembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    
                    .LifestyleSingleton()
                );  
            //Windsor Interceptors
            IocManager.IocContainer.Register(
              Classes.FromAssembly(CurrentAssembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<IInterceptor>()
                    .WithService.Self()
                    .LifestyleTransient()
                );
            Register(container, store);          
             
           
            
        }
    }
}
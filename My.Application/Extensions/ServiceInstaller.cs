
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using My.Core;
using My.Domain;
using System.Web;
using System.Linq;
using System.Reflection;
using My.Core.UnitOfWork;
using System;
namespace My.Application
{
   
    public class ServiceInstaller : DefaultInstaller
    {
        public ServiceInstaller() {
        
        }
        public override void Register(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssembly(CurrentAssembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<IService>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );



            base.Register(container, store);
        }
        void SeedData() {
           // var repo = IocManager.Resolve<Repository>();
           // var c=new Customer{
           //   Name="123"
           // };
           //repo.Save(c);
        }
    }
}
using System.Web.Mvc;

namespace DataShow.Areas.Manage
{
    public class ManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Manage",
                "Manage/{controller}/{action}/{id}",
                new {controller= "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "DataShow.Manage.Controllers.*" }
            );

        }
    }
}

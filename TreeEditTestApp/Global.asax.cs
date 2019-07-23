using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TreeEditTestApp.Data.Configuration;
using Unity;

namespace TreeEditTestApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = CreateUnityContainer();

            AreaRegistration.RegisterAllAreas();

            NHibernateConfig.ConfigureDatabase(container);
            UnityConfig.RegisterComponents(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private IUnityContainer CreateUnityContainer()
        {
            return new UnityContainer();
        }
    }
}

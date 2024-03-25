using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UICRMVulnerables
{
    /// <summary>
    /// Clase de definida para el Global.asax.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Método de inicialización del aplicativo.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
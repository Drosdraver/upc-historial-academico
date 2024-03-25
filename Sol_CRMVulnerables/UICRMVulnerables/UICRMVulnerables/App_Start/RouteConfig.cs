using System.Web.Mvc;
using System.Web.Routing;

namespace UICRMVulnerables
{
    /// <summary>
    /// Clase de configuración para el mapeo de rutas.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Método que mapea las rutas y define el controlador y la acción por defecto a realizar.
        /// </summary>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HistorialAcademico", action = "Inicio", id = UrlParameter.Optional }
            );
        }
    }
}
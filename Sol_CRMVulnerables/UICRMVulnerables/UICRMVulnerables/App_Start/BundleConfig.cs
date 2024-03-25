using System.Web;
using System.Web.Optimization;

namespace UICRMVulnerables
{
    /// <summary>
    /// Clase de configuración para los conjuntos de scripts y estilos a emplear.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Método que registra los scripts y estilos a usar en las vistas.
        /// </summary>
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            // Script Bundles

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/collapse").Include(
                        "~/Scripts/collapse.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                        "~/Scripts/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropdown").Include(
                        "~/Scripts/dropdown.js"));

            bundles.Add(new ScriptBundle("~/bundles/fusioncharts").Include(
                        "~/Scripts/fusioncharts.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                        "~/Scripts/main*"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                        "~/Scripts/modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/nav").Include(
                        "~/Scripts/nav-desktop.js"));

            bundles.Add(new ScriptBundle("~/bundles/tiny").Include(
                       "~/Scripts/tiny*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/vistaInicio").Include(
                      "~/Scripts/mensajeError.js",
                      "~/Scripts/vistaInicio.js",
                      "~/Scripts/procesosVistas.js"));

            bundles.Add(new ScriptBundle("~/bundles/vistaNotasActuales").Include(
                      "~/Scripts/mensajeError.js",
                      "~/Scripts/vistaNotasActuales.js",
                      "~/Scripts/procesosVistas.js"));

            bundles.Add(new ScriptBundle("~/bundles/vistaInasistencias").Include(
                      "~/Scripts/mensajeError.js",
                      "~/Scripts/vistaInasistencias.js",
                      "~/Scripts/procesosVistas.js"));

            bundles.Add(new ScriptBundle("~/bundles/vistaPagosPendientes").Include(
                      "~/Scripts/mensajeError.js",
                      "~/Scripts/vistaPagosPendientes.js",
                      "~/Scripts/procesosVistas.js"));

            // Style Bundles

            bundles.Add(new Bundle("~/Content/css").Include(
                      "~/Content/main.css",
                      "~/Content/main.mia.css",
                      "~/Content/bootstrap.mia.css",
                      "~/Content/customizations.css",
                      "~/Content/fuentes.css"));
        }
    }
}
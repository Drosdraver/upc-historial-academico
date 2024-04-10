using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using FNT_BusinessEntities;
using FNT_BusinessLogic;

namespace UICRMVulnerables.Controllers
{
    /// <summary>
    /// Controlador de las 4 vistas y los servicios POST que manejan.
    /// </summary>
    public class HistorialAcademicoController : Controller
    {
        /// <summary>
        /// Acción que invoca a la vista "Inicio", la cual contiene a todas las pestañas del aplicativo. <br />
        /// Ruta de acceso: http://[ruta-upc-vulnerables]/HistorialAcademico/Inicio?[parametros-url]
        /// </summary>
        public ActionResult Inicio()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["PageTitle"];
            return View();
        }

        /// <summary>
        /// Acción que invoca a la vista "NotasActuales", la cual contiene solamente la pestaña de "Notas actuales". <br />
        /// Ruta de acceso: http://[ruta-upc-vulnerables]/HistorialAcademico/NotasActuales?[parametros-url]
        /// </summary>
        public ActionResult NotasActuales()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["PageTitleNotasActuales"];
            return View();
        }

        /// <summary>
        /// Acción que invoca a la vista "Inasistencias", la cual contiene solamente la pestaña de "Inasistencias". <br />
        /// Ruta de acceso: http://[ruta-upc-vulnerables]/HistorialAcademico/Inasistencias?[parametros-url]
        /// </summary>
        public ActionResult Inasistencias()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["PageTitleInasistencias"];
            return View();
        }

        /// <summary>
        /// Acción que invoca a la vista "PagosPendientes", la cual contiene solamente la pestaña de "Deudas". <br />
        /// Ruta de acceso: http://[ruta-upc-vulnerables]/HistorialAcademico/PagosPendientes?[parametros-url]
        /// </summary>
        public ActionResult PagosPendientes()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["PageTitlePagosPendientes"];
            return View();
        }

        /// <summary>
        /// Servicio POST que retorna el valor de la llave "UrlCRM" del Web.config.
        /// </summary>
        [HttpPost]
        public ActionResult UrlCRM()
        {
            return Content(ConfigurationManager.AppSettings["UrlCRM"]);
        }

        /// <summary>
        /// Servicio POST que retorna un JSON con todos los valores necesarios para poblar la vista "Inicio".
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> HistorialAcademicoResultado(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            DTOHistorialAcademico oDTOHistorialAcademico = new DTOHistorialAcademico();
            HistorialAcademicoBusinessLogic historial = new HistorialAcademicoBusinessLogic();
            oDTOHistorialAcademico = await historial.getHistorialAcademicoAsync(pc_CodLineaNegocio, pc_CodAlumno, pc_CodModalEst, pc_CodPeriodo);

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(oDTOHistorialAcademico), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Servicio POST que retorna un JSON con todos los valores necesarios para poblar la vista "NotasActuales".
        /// </summary>
        [HttpPost]
        public JsonResult NotasActualesResultado(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            DTOHistorialAcademico oDTONotasActuales = new DTOHistorialAcademico();
            oDTONotasActuales = HistorialAcademicoBusinessLogic.getNotasActuales(pc_CodLineaNegocio, pc_CodAlumno, pc_CodModalEst, pc_CodPeriodo);

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(oDTONotasActuales), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Servicio POST que retorna un JSON con todos los valores necesarios para poblar la vista "Inasistencias".
        /// </summary>
        [HttpPost]
        public JsonResult InasistenciasResultado(string pc_CodLineaNegocio, string pc_CodAlumno, string pc_CodModalEst, string pc_CodPeriodo)
        {
            DTOHistorialAcademico oDTOInasistencias = new DTOHistorialAcademico();
            oDTOInasistencias = HistorialAcademicoBusinessLogic.getInasistencias(pc_CodLineaNegocio, pc_CodAlumno, pc_CodModalEst, pc_CodPeriodo);

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(oDTOInasistencias), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Servicio POST que retorna un JSON con todos los valores necesarios para poblar la vista "PagosPendientes".
        /// </summary>
        [HttpPost]
        public JsonResult PagosPendientesResultado(string pc_CodLineaNegocio, string pc_CodAlumno)
        {
            DTOHistorialAcademico oDTOPagosPendientes = new DTOHistorialAcademico();
            oDTOPagosPendientes = HistorialAcademicoBusinessLogic.getPagosPendientes(pc_CodLineaNegocio, pc_CodAlumno);

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(oDTOPagosPendientes), JsonRequestBehavior.AllowGet);
        }
    }
}

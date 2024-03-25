using System;
using System.Configuration;
using System.Net;
using System.Net.Security;
using Newtonsoft.Json;
using FNT_BusinessEntities;
using FNT_BusinessEntities.WebServiceRespuesta;
using FNT_Common.Enum;
using FNT_Common.Resources;
using FNT_Common;

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Alumno Facturable.
    /// </summary>
    public class AlumnosFacturablesBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Alumnos Facturables.
        /// </summary>
        public static DTOAlumnosFacturablesRespuesta getAlumnosFacturables(string pc_codLineaNegocio, string pc_codAlumno, string pc_codModalEst, string pc_codPeriodo)
        {
            DTOAlumnosFacturablesRespuesta alumnosFacturables = new DTOAlumnosFacturablesRespuesta();
            alumnosFacturables.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJAlumnosFacturables;
                alumnosFacturables = JsonConvert.DeserializeObject<DTOAlumnosFacturablesRespuesta>(dataEjemplo);
                alumnosFacturables.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                alumnosFacturables.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getAlumnosFacturables");

                return alumnosFacturables;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_AlumnosFacturables"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}&CodModalEst={2}&CodPeriodo={3}", pc_codLineaNegocio, pc_codAlumno, pc_codModalEst, pc_codPeriodo);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                alumnosFacturables = JsonConvert.DeserializeObject<DTOAlumnosFacturablesRespuesta>(data);

                //alumnosFacturables.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //alumnosFacturables.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getAlumnosFacturables");
            }
            catch (Exception ex)
            {
                alumnosFacturables.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //alumnosFacturables.DTOHeader.DescRetorno = ex.Message.ToString();
                alumnosFacturables.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return alumnosFacturables;
        }
    }
}

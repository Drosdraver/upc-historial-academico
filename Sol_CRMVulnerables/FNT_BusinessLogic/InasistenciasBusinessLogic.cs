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
    /// Clase de la capa lógica de la entidad Inasistencias.
    /// </summary>
    public class InasistenciasBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Inasistencias.
        /// </summary>
        public static DTOInasistenciasResultado getInasistencias(string pc_codLineaNegocio, string pc_codAlumno, string pc_codModalEst, string pc_codPeriodo)
        {
            DTOInasistenciasResultado inasistencias = new DTOInasistenciasResultado();
            inasistencias.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJInasistencias;
                inasistencias = JsonConvert.DeserializeObject<DTOInasistenciasResultado>(dataEjemplo);
                inasistencias.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                inasistencias.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getInasistencias");

                return inasistencias;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_Inasistencias"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}&CodModalEst={2}&CodPeriodo={3}", pc_codLineaNegocio, pc_codAlumno, pc_codModalEst, pc_codPeriodo);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                inasistencias = JsonConvert.DeserializeObject<DTOInasistenciasResultado>(data);

                //inasistencias.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //inasistencias.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getInasistencias");
            }
            catch (Exception ex)
            {
                inasistencias.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //inasistencias.DTOHeader.DescRetorno = ex.Message.ToString();
                inasistencias.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return inasistencias;
        }
    }
}

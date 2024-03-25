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
    /// Clase de la capa lógica de la entidad Avance Notas.
    /// </summary>
    public class AvanceNotasBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Avance Notas.
        /// </summary>
        public static DTOAvanceNotasRespuesta getAvanceNotas(string pc_codLineaNegocio, string pc_codAlumno, string pc_codModalEst, string pc_codPeriodo)
        {
            DTOAvanceNotasRespuesta avanceNotas = new DTOAvanceNotasRespuesta();
            avanceNotas.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJAvanceNotas;
                avanceNotas = JsonConvert.DeserializeObject<DTOAvanceNotasRespuesta>(dataEjemplo);
                avanceNotas.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                avanceNotas.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getAvanceNotas");

                return avanceNotas;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_AvanceNotas"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}&CodModalEst={2}&CodPeriodo={3}", pc_codLineaNegocio, pc_codAlumno, pc_codModalEst, pc_codPeriodo);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                avanceNotas = JsonConvert.DeserializeObject<DTOAvanceNotasRespuesta>(data);

                //avanceNotas.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //avanceNotas.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getAvanceNotas");
            }
            catch (Exception ex)
            {
                avanceNotas.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //avanceNotas.DTOHeader.DescRetorno = ex.Message.ToString();
                avanceNotas.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return avanceNotas;
        }
    }
}

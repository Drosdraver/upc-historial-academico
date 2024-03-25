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
    /// Clase de la capa lógica de la entidad Hechos Importantes.
    /// </summary>
    public class HechosImportantesBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Hechos Importantes.
        /// </summary>
        public static DTOHechosImportantesRespuesta getHechosImportantes(string pc_codLineaNegocio, string pc_codAlumno)
        {
            DTOHechosImportantesRespuesta hechosImportantes = new DTOHechosImportantesRespuesta();
            hechosImportantes.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJHechosImportantes;
                hechosImportantes = JsonConvert.DeserializeObject<DTOHechosImportantesRespuesta>(dataEjemplo);
                hechosImportantes.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                hechosImportantes.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getHechosImportantes");

                return hechosImportantes;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_HechosImportantes"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}", pc_codLineaNegocio, pc_codAlumno);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                hechosImportantes = JsonConvert.DeserializeObject<DTOHechosImportantesRespuesta>(data);

                //hechosImportantes.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //hechosImportantes.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getHechosImportantes");
            }
            catch (Exception ex)
            {
                hechosImportantes.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //hechosImportantes.DTOHeader.DescRetorno = ex.Message.ToString();
                hechosImportantes.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return hechosImportantes;
        }
    }
}

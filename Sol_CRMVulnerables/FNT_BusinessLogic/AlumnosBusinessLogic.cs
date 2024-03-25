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
    /// Clase de la capa lógica de la entidad Alumno.
    /// </summary>
    public class AlumnosBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Alumnos.
        /// </summary>
        public static DTOAlumnosRespuesta getAlumnos(string pc_codLineaNegocio, string pc_codAlumno)
        {
            DTOAlumnosRespuesta alumnos = new DTOAlumnosRespuesta();
            alumnos.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJAlumnos;
                alumnos = JsonConvert.DeserializeObject<DTOAlumnosRespuesta>(dataEjemplo);
                alumnos.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                alumnos.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getAlumnos");

                return alumnos;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_Alumnos"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}", pc_codLineaNegocio, pc_codAlumno);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                alumnos = JsonConvert.DeserializeObject<DTOAlumnosRespuesta>(data);

                //alumnos.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //alumnos.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getAlumnos");
            }
            catch (Exception ex)
            {
                alumnos.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //alumnos.DTOHeader.DescRetorno = ex.Message.ToString();
                alumnos.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return alumnos;
        }
    }
}

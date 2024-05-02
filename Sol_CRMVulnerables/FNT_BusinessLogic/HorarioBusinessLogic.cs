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
    /// Clase de la capa lógica de la entidad Horario.
    /// </summary>
    public class HorarioBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Horarios.
        /// </summary>
        public static DTOHorarioAlumnoResultado getHorario(string pc_codLineaNegocio, string pc_codAlumno, string pc_codModalEst, string pc_codPeriodo, string pc_fechaSesion1, string pc_fechaSesion2)
        {
            DTOHorarioAlumnoResultado horario = new DTOHorarioAlumnoResultado();
            horario.DTOHeader = new DTOHeader();

            if (!ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJHorario;
                horario = JsonConvert.DeserializeObject<DTOHorarioAlumnoResultado>(dataEjemplo);
                horario.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                horario.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getHorario");

                return horario;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_Horarios"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}&CodModalEst={2}&CodPeriodo={3}&FechaSesion1={4}&FechaSesion2={5}", pc_codLineaNegocio, pc_codAlumno, pc_codModalEst, pc_codPeriodo, pc_fechaSesion1, pc_fechaSesion2);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                horario = JsonConvert.DeserializeObject<DTOHorarioAlumnoResultado>(data);

                //horario.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //horario.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getHorario");
            }
            catch (Exception ex)
            {
                horario.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //horario.DTOHeader.DescRetorno = ex.Message.ToString();
                horario.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return horario;
        }
    }
}

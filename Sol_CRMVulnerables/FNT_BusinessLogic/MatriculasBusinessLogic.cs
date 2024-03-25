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
    /// Clase de la capa lógica de la entidad Matricula.
    /// </summary>
    public class MatriculasBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Matriculas.
        /// </summary>
        public static DTOMatriculasRespuesta getMatriculas(string pc_codLineaNegocio, string pc_codAlumno)
        {
            DTOMatriculasRespuesta matriculas = new DTOMatriculasRespuesta();
            matriculas.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJMatriculas;
                matriculas = JsonConvert.DeserializeObject<DTOMatriculasRespuesta>(dataEjemplo);
                matriculas.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                matriculas.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getMatriculas");

                return matriculas;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_Matriculas"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}", pc_codLineaNegocio, pc_codAlumno);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                matriculas = JsonConvert.DeserializeObject<DTOMatriculasRespuesta>(data);

                //matriculas.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //matriculas.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getMatriculas");
            }
            catch (Exception ex)
            {
                matriculas.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //matriculas.DTOHeader.DescRetorno = ex.Message.ToString();
                matriculas.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return matriculas;
        }
    }
}

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
    /// Clase de la capa lógica de la entidad Detalle Matricula.
    /// </summary>
    public class DetalleMatriculaBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Detalle de Matrícula.
        /// </summary>
        public static DTODetMatriculaResultado getDetalleMatricula(string pc_codLineaNegocio, string pc_codAlumno, string pc_codModalEst, string pc_codPeriodo)
        {
            DTODetMatriculaResultado detalleMatricula = new DTODetMatriculaResultado();
            detalleMatricula.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJDetalleMatricula;
                detalleMatricula = JsonConvert.DeserializeObject<DTODetMatriculaResultado>(dataEjemplo);
                detalleMatricula.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                detalleMatricula.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getDetalleMatricula");

                return detalleMatricula;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_DetalleMatriculas"] +
                    String.Format("?CodLineaNegocio={0}&CodAlumno={1}&CodModalEst={2}&CodPeriodo={3}", pc_codLineaNegocio, pc_codAlumno, pc_codModalEst, pc_codPeriodo);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                detalleMatricula = JsonConvert.DeserializeObject<DTODetMatriculaResultado>(data);

                //detalleMatricula.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //detalleMatricula.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getDetalleMatricula");
            }
            catch (Exception ex)
            {
                detalleMatricula.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //detalleMatricula.DTOHeader.DescRetorno = ex.Message.ToString();
                detalleMatricula.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return detalleMatricula;
        }
    }
}

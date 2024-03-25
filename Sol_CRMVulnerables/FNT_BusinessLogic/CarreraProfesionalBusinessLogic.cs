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
    /// Clase de la capa lógica de la entidad Carrera Profesional.
    /// </summary>
    public class CarreraProfesionalBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Carrera Profesional.
        /// </summary>
        public static DTOCarreraProfesionalRespuesta getCarreraProfesional(string pc_codLineaNegocio, string pc_codModalEst, string pc_codProducto)
        {
            DTOCarreraProfesionalRespuesta carreraProfesional = new DTOCarreraProfesionalRespuesta();
            carreraProfesional.oDTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJCarreraProfesional;
                carreraProfesional = JsonConvert.DeserializeObject<DTOCarreraProfesionalRespuesta>(dataEjemplo);
                carreraProfesional.oDTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                carreraProfesional.oDTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getCarreraProfesional");

                return carreraProfesional;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_CarreraProfesional"] +
                    String.Format("?CodLineaNegocio={0}&CodModalEst={1}&CodProducto={2}", pc_codLineaNegocio, pc_codModalEst, pc_codProducto);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                carreraProfesional = JsonConvert.DeserializeObject<DTOCarreraProfesionalRespuesta>(data);

                //carreraProfesional.oDTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //carreraProfesional.oDTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getCarreraProfesional");
            }
            catch (Exception ex)
            {
                carreraProfesional.oDTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //carreraProfesional.oDTOHeader.DescRetorno = ex.Message.ToString();
                carreraProfesional.oDTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return carreraProfesional;
        }
    }
}

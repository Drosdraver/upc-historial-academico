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
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using FNT_BusinessEntities.WebServiceRespuesta.Banner;
using static FNT_Common.ConexionServicio;

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Detalle Matricula.
    /// </summary>
    public class DetalleMatriculaBusinessLogic
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_Banner"]) };
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

        /// <summary>
        /// Método que obtiene data del servicio de Detalle de Matrícula de Banner.
        /// </summary>
        /// <param name="pc_bearer_token"></param>
        /// <param name="pc_cod_nivel"></param>
        /// <param name="pc_cod_programa"></param>
        /// <param name="pc_cod_alumno"></param>
        /// <returns></returns>
        public async Task<DTODetalleMatriculaBannerRespuesta> getDetalleMatriculaBanner(string pc_cod_nivel, string pc_cod_programa, string pc_cod_alumno)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponse token = await conexion.GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await _httpClient.GetAsync($"/Academico/v4.0/DetalleMatricula?CodigoNivel={pc_cod_nivel}&CodigoPrograma={pc_cod_programa}&CodigoAlumno={pc_cod_alumno}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTODetalleMatriculaBannerRespuesta>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de detalle de matrícula en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

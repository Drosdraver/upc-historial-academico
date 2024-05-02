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
using static FNT_Common.ConexionServicio;
using System.Net.Http;
using System.Net.Http.Headers;

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
        /// 
        private readonly HttpClient _httpClient= new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_uapi"]) };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pc_codLineaNegocio"></param>
        /// <param name="pc_codAlumno"></param>
        /// <param name="pc_codModalEst"></param>
        /// <param name="pc_codPeriodo"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Servicio que obtiene la lista de inasistencias del alumno por curso
        /// </summary>
        /// <param name="pc_cod_periodo"></param>
        /// <param name="pc_cod_alumno"></param>
        /// <param name="pc_cod_curso"></param>
        /// <returns></returns>
        public async Task<DTOInasistenciasResultado> getInasistenciasUapi(string pc_cod_periodo, string pc_cod_alumno, string pc_cod_curso)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponseuapi token = await conexion.GetTokenUClassAsync();
            string SubscriptionKey = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key-U-class"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var response = await _httpClient.GetAsync($"/classdemo/v2.1/non-attendance-students?CodPeriodo={pc_cod_periodo}&CodAlumno={pc_cod_alumno}&CodCurso={pc_cod_curso}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTOInasistenciasResultado>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de las inasistencias en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

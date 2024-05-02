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
using FNT_BusinessEntities.WebServiceRespuesta.Banner;
using System.Net.Http.Headers;
using static FNT_Common.ConexionServicio;
using System.Net.Http;

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Alumno.
    /// </summary>
    public class AlumnosBusinessLogic
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_Banner"]) };
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
        /// <summary>
        /// Método para obtener la data del servicio de Alumnos en Banner
        /// </summary>
        /// <param name="pc_cod_nivel"></param>
        /// <param name="pc_cod_alumno"></param>
        /// <returns></returns>
        public async Task<DTOAlumnosRespuestaBanner> getAlumnosBanner(string pc_cod_nivel, string pc_cod_alumno)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponse token = await conexion.GetTokenAsync();
            string SubscriptionKey = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key-Banner"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var response = await _httpClient.GetAsync($"/Academico/v4.0/Alumno?CodigoNivel={pc_cod_nivel}&CodigoAlumno={pc_cod_alumno}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTOAlumnosRespuestaBanner>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de alumno en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

using FNT_BusinessEntities.WebServiceRespuesta.Banner;
using FNT_Common;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static FNT_Common.ConexionServicio;

namespace FNT_BusinessLogic
{
    public class HistoriaAlumnoBusinessLogic
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_Banner"]) };
        /// <summary>
        /// Método para obtener los datos de la Historia del Alumno en Banner
        /// </summary>
        /// <param name="pc_cod_nivel"></param>
        /// <param name="pc_cod_programa"></param>
        /// <param name="pc_pidm"></param>
        /// <returns></returns>
        public async Task<DTOHistoriaAlumnoBannerRespuesta> getHistoriaAlumnoBanner(string pc_cod_nivel, string pc_cod_programa, int pc_pidm)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponse token = await conexion.GetTokenAsync();
            string SubscriptionKey = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key-Banner"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var response = await _httpClient.GetAsync($"/Academico/v4.0/HistoriaAlumno?CodigoNivel={pc_cod_nivel}&CodigoPrograma={pc_cod_programa}&PIDM={pc_pidm}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTOHistoriaAlumnoBannerRespuesta>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de detalle de matrícula en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

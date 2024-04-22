using FNT_BusinessEntities.WebServiceRespuesta.Banner;
using FNT_Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static FNT_Common.ConexionServicio;

namespace FNT_BusinessLogic
{
    public class NotasActualesBusinessLogic
    {
        /// <summary>
        /// Variable global
        /// </summary>
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_uapi"]) };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pc_cod_periodo"></param>
        /// <param name="pc_cod_alumno"></param>
        /// <param name="pc_cod_curso"></param>
        /// <returns></returns>
        public async Task<DTONotasActualesRespuesta> getNotasActualesUapi(string pc_cod_periodo, string pc_cod_alumno, string pc_cod_curso)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponseuapi token = await conexion.GetTokenUClassAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            var response = await _httpClient.GetAsync($"/api/class-api/report/advancement-notes?CodPeriodo={pc_cod_periodo}&CodAlumno={pc_cod_alumno}&CodCurso={pc_cod_curso}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTONotasActualesRespuesta>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de las notas actuales de banner en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

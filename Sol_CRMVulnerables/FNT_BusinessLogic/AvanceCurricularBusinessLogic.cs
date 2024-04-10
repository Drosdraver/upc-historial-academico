﻿using FNT_BusinessEntities.WebServiceRespuesta.Banner;
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
    public class AvanceCurricularBusinessLogic
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_Banner"]) };

        public async Task<DTOAvanceCurricularBannerRespuesta> getAvanceCurricularBanner(string pc_cod_nivel, string pc_cod_alumno, string pc_cod_programa)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponse token = await conexion.GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await _httpClient.GetAsync($"/Academico/v4.0/AvanceCurricular?CodigoNivel={pc_cod_nivel}&CodigoAlumno={pc_cod_alumno}&CodigoPrograma={pc_cod_programa}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTOAvanceCurricularBannerRespuesta>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de matrícula en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}
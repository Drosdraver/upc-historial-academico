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
using System.Net.Http.Headers;
using System.Net.Http;
using static FNT_Common.ConexionServicio;
using FNT_BusinessEntities.WebServiceRespuesta.Banner;

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Matricula.
    /// </summary>
    public class MatriculasBusinessLogic
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_Banner"]) };
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

        public async Task<DTOMatriculaBannerRespuesta> getMatriculasBanner(string pc_cod_nivel, string pc_id_banner, string pc_cod_programa)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponse token = await conexion.GetTokenAsync();
            string SubscriptionKey = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key-Banner"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var response = await _httpClient.GetAsync($"/Academico/v4.0/Matricula?CodigoNivel={pc_cod_nivel}&IdBanner={pc_id_banner}&CodigoPrograma={pc_cod_programa}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var alumnoResponse = JsonConvert.DeserializeObject<DTOMatriculaBannerRespuesta>(jsonString);
                return alumnoResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de matrícula en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

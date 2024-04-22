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
using FNT_BusinessEntities.WebServiceRespuesta.Banner;
using System.Threading.Tasks;
using static FNT_Common.ConexionServicio;
using System.Net.Http.Headers;
using System.Net.Http;

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Malla Curricular.
    /// </summary>
    public class MallaCurricularBusinessLogic
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_Banner"]) };
        /// <summary>
        /// Método que obtiene data del servicio de Mallas Curriculares.
        /// </summary>
        public static DTOMallaCurricularRespuesta getMallaCurricular(string pc_codLineaNegocio, string pc_codProducto, string codCurriculo)
        {
            DTOMallaCurricularRespuesta mallaCurricular = new DTOMallaCurricularRespuesta();
            mallaCurricular.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJMallaCurricular;
                mallaCurricular = JsonConvert.DeserializeObject<DTOMallaCurricularRespuesta>(dataEjemplo);
                mallaCurricular.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                mallaCurricular.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getMallaCurricular");

                return mallaCurricular;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    ConfigurationManager.AppSettings["Ws_MallasCurriculares"] +
                    String.Format("?CodLineaNegocio={0}&CodProducto={1}&CodCurriculo={2}", pc_codLineaNegocio, pc_codProducto, codCurriculo);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                mallaCurricular = JsonConvert.DeserializeObject<DTOMallaCurricularRespuesta>(data);

                //mallaCurricular.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                //mallaCurricular.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getMallaCurricular");
            }
            catch (Exception ex)
            {
                mallaCurricular.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //mallaCurricular.DTOHeader.DescRetorno = ex.Message.ToString();
                mallaCurricular.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return mallaCurricular;
        }

        public async Task<DTOMallaCurricularBannerRespuesta> getMallaCurricularBanner(string pc_cod_periodo, string pc_cod_programa)
        {
            ConexionServicio conexion = new ConexionServicio();
            TokenResponse token = await conexion.GetTokenAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await _httpClient.GetAsync($"/Academico/v4.0/MallaCurricular?CodigoPeriodo={pc_cod_periodo}&CodigoPrograma={pc_cod_programa}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var mallaResponse = JsonConvert.DeserializeObject<DTOMallaCurricularBannerRespuesta>(jsonString);
                return mallaResponse;
            }
            else
            {
                throw new Exception($"Error al obtener los datos de malla curricular de banner en el servicio. Status code: {response.StatusCode}");
            }
        }
    }
}

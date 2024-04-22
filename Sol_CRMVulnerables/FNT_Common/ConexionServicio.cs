using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FNT_Common
{
    /// <summary>
    /// Clase de configuración de la conexión con los servicios.
    /// </summary>
    public class ConexionServicio
    {
        private readonly HttpClient _httpClientInasistencias = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Servidor_ws_uapi"]) };

        /// <summary>
        /// Configuración del WebClient instanciado por los servicios.
        /// </summary>
        public static WebClient CurrentWebClientConfig()
        {
            string usr = ConfigurationManager.AppSettings["USR_SERVD"];
            string pass = ConfigurationManager.AppSettings["PASS_SERVD"];

            WebClient webClient = new WebClient();
            webClient.UseDefaultCredentials = true;
            webClient.Credentials = new NetworkCredential(usr, pass);
            webClient.Headers.Add("Content-Type", "application/json");
            webClient.Encoding = Encoding.UTF8;

            return webClient;
        }

        /// <summary>
        /// Método empleado para instanciar ServicePointManager.ServerCertificateValidationCallback.
        /// </summary>
        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Obtener el token de los servicios de Banner https://servicioswebdesa-manager.upc.edu.pe
        /// </summary>
        /// <returns></returns>
        public async Task<TokenResponse> GetTokenAsync()
        {
            string ApiUrl = ConfigurationManager.AppSettings["Generar_Token_Banner"];
            string SubscriptionKey = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key-Banner"];

            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

                var response = await client.GetAsync(ApiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonString);
                    return tokenResponse;
                }
                else
                {
                    throw new Exception($"Failed to get token. Status code: {response.StatusCode}");
                }
            }
        }
        /// <summary>
        /// Obtener el Token de las inasistencias en al api https://upc-e2g-post-demo-api.gateway.u-planner.com
        /// </summary>
        /// <returns></returns>
        public async Task<TokenResponseuapi> GetTokenUClassAsync()
        {
            var requestBody = new
            {
                client_id = ConfigurationManager.AppSettings["client_id"],
                client_secret = ConfigurationManager.AppSettings["client_secret"],
                grant_type = ConfigurationManager.AppSettings["grant_type"]
            };

            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClientInasistencias.PostAsync("/oauth2/token", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseuapi>(responseContent);
                return tokenResponse;
            }
            else
            {
                throw new HttpRequestException($"Error al obtener el token del servidor https://upc-e2g-post-demo-api.gateway.u-planner.com. Código de estado: {response.StatusCode}");
            }
        }

        #region Clases locales
        /// <summary>
        /// Clase tipada para obtener el response del servidor https://servicioswebdesa-manager.upc.edu.pe
        /// </summary>
        public class TokenResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public DateTime Expiration { get; set; }
            public string User { get; set; }
            public string Status { get; set; }
        }

        /// <summary>
        /// Clase tipada para obtener el response del servidor https://upc-e2g-post-demo-api.gateway.u-planner.com
        /// </summary>
        public class TokenResponseuapi
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
        }

        #endregion

    }
}


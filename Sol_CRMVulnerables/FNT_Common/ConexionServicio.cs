using System;
using System.Configuration;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FNT_Common
{
    /// <summary>
    /// Clase de configuración de la conexión con los servicios.
    /// </summary>
    public class ConexionServicio
    {
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
    }
}

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

namespace FNT_BusinessLogic
{
    /// <summary>
    /// Clase de la capa lógica de la entidad Clientes.
    /// </summary>
    public class ClientesBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Clientes.
        /// </summary>
        public static DTOClientesResultado getClientes(string pc_codUsuario)
        {
            DTOClientesResultado clientes = new DTOClientesResultado();

            if (!ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJCliente;
                clientes = JsonConvert.DeserializeObject<DTOClientesResultado>(dataEjemplo);

                clientes.DTOHeader = new DTOHeader();
                clientes.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                clientes.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getClientes");

                return clientes;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws_deudas"] +
                    ConfigurationManager.AppSettings["Ws_Cliente"] +
                    String.Format("-/{0}/-/-/-", pc_codUsuario);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var data = webClient.DownloadString(url);
                clientes = JsonConvert.DeserializeObject<DTOClientesResultado>(data);

                clientes.DTOHeader = new DTOHeader();
                clientes.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                if (clientes.Resutado == 1)
                    clientes.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getClientes");
                else
                    clientes.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.ResultadoAnomalo, "getClientes");
            }
            catch (Exception ex)
            {
                clientes.DTOHeader = new DTOHeader();
                clientes.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //clientes.DTOHeader.DescRetorno = ex.Message.ToString();
                clientes.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return clientes;
        }
    }
}

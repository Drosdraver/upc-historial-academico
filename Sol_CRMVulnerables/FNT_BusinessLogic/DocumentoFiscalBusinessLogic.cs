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
    /// Clase de la capa lógica de la entidad Documento Fiscal.
    /// </summary>
    public class DocumentoFiscalBusinessLogic
    {
        /// <summary>
        /// Método que obtiene data del servicio de Documentos Fiscales.
        /// </summary>
        public static DTODocumentoFiscalResultado getDocumentoFiscal(string pc_clienteCobrara)
        {
            DTODocumentoFiscalResultado documentoFiscal = new DTODocumentoFiscalResultado();

            if (!ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJDocumentoFiscal;
                documentoFiscal = JsonConvert.DeserializeObject<DTODocumentoFiscalResultado>(dataEjemplo);

                documentoFiscal.DTOHeader = new DTOHeader();
                documentoFiscal.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                documentoFiscal.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getDocumentoFiscal");

                return documentoFiscal;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws_deudas"] +
                    ConfigurationManager.AppSettings["Ws_DocumentoFiscal"] +
                    String.Format("{0}/-/-/-/-/-/-/{1}", pc_clienteCobrara, ConfigurationManager.AppSettings["CodigoDeudasPendientes"]);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var data = webClient.DownloadString(url);
                documentoFiscal = JsonConvert.DeserializeObject<DTODocumentoFiscalResultado>(data);

                documentoFiscal.DTOHeader = new DTOHeader();
                documentoFiscal.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                if (documentoFiscal.Resutado == 1)
                    documentoFiscal.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getDocumentoFiscal");
                else
                    documentoFiscal.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.ResultadoAnomalo, "getDocumentoFiscal");
            }
            catch (Exception ex)
            {
                documentoFiscal.DTOHeader = new DTOHeader();
                documentoFiscal.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                //documentoFiscal.DTOHeader.DescRetorno = ex.Message.ToString();
                documentoFiscal.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return documentoFiscal;
        }
    }
}

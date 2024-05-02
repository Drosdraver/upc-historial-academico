using FNT_BusinessEntities;
using FNT_BusinessEntities.WebServiceRespuesta;
using FNT_Common;
using FNT_Common.Enum;
using FNT_Common.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessLogic
{
    public class TramitesBusinessLogic
    {
        public static DTOTramitesResultado getTramites(string servicio, string pc_cod_linea_negocio, string pc_cod_modalidad, string pc_cod_alumno)
        {
            DTOTramitesResultado tramites = new DTOTramitesResultado();
            tramites.DTOHeader = new DTOHeader();

            if (ExampleData.UsarDataEjemplo)
            {
                String dataEjemplo = ExampleData.EJTramites;
                tramites = JsonConvert.DeserializeObject<DTOTramitesResultado>(dataEjemplo);
                tramites.DTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                tramites.DTOHeader.DescRetorno = String.Format("{0}: {1}", Messages.LecturaDatosExitosa, "getTramites");

                return tramites;
            }

            try
            {
                var url =
                    ConfigurationManager.AppSettings["Servidor_ws"] +
                    servicio +
                    String.Format("?CodLineaNegocio={0}&CodModalEst={1}&CodAlumno={2}", pc_cod_linea_negocio, pc_cod_modalidad, pc_cod_alumno);

                WebClient webClient = ConexionServicio.CurrentWebClientConfig();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ConexionServicio.ValidateServerCertificate);
                var data = webClient.DownloadString(url);
                tramites = JsonConvert.DeserializeObject<DTOTramitesResultado>(data);
            }
            catch (Exception ex)
            {
                tramites.DTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                tramites.DTOHeader.DescRetorno = Messages.ErrorUsoServicios;
            }

            return tramites;
        }
    }
}

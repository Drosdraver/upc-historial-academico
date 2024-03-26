using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    public class DTOTramitesResultado
    {
        public DTOHeader DTOHeader { get; set; }
        public List<TramiteAlumno> ListaTramiteAlumno { get; set; }
    }

    public class TramiteAlumno
    {
        public string CodAlumno { get; set; }
        public string CodLineaNegocio { get; set; }
        public int CodSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public string MotivoSolicitud { get; set; }
        public object ObservacionSolicitud { get; set; }
        public int IdTramite { get; set; }
        public string NombreTramite { get; set; }
        public string EstadoSolicitud { get; set; }
        public object DescEstadoSolicitud { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificador { get; set; }
        public DateTime FechaModificacion { get; set; }
        public object CodUltimaActividad { get; set; }
        public object DescripcionUtimaActividad { get; set; }
        public object EstadoUltimaActividad { get; set; }
        public object DescUltimaActividad { get; set; }
        public List<object> ListaActividades { get; set; }
    }
}

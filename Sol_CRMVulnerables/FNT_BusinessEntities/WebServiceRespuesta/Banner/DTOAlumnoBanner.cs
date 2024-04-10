using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta.Banner
{
    public class DTOAlumnosRespuestaBanner
    {
        public DTOHeaderBanner cabecera { get; set; }
        public DTODetalleAlumnoBanner detalle { get; set; }
    }

    public class DTODetalleAlumnoBanner
    {
        public List<AlumnoBanner> listaAlumno { get; set; }
    }

    public class AlumnoBanner
    {
        public string idBanner { get; set; }
        public int pidm { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string sexo { get; set; }
        public Lista tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string telefono { get; set; }
        public string codigoAlumno { get; set; }
        public string codigoPersona { get; set; }
        public string codigoUsuario { get; set; }
        public string fotoAlumno { get; set; }
        public string correoElectronico { get; set; }
        public Lista nivel { get; set; }
        public Lista campus { get; set; }
        public Lista estadoAlumno { get; set; }
        public List<Programa> programas { get; set; }
        public string fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
    }

    public class Programa
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public int numeroReglaBase { get; set; }
        public string codigoPeriodo { get; set; }
        public string codigoPeriodoCatalogo { get; set; }
        public string descripcionEspecial { get; set; }
        public Lista estadoPlanEstudio { get; set; }
        public Lista major { get; set; }
        public Lista estadoCampoEstudio { get; set; }
        public Lista tipoAlumno { get; set; }
        public Lista tipoAprendizaje { get; set; }
        public Lista tipoAdmision { get; set; }
        public List<CodigoLista> tipoCohorte { get; set; }
        public List<object> tipoAtributoAlumno { get; set; }
    }

    public class CodigoLista
    {
        public string codigoPeriodo { get; set; }
        public List<Lista> lista { get; set; }
    }

    public class Lista
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta.Banner
{
    public class DTOMatriculaBannerRespuesta
    {
        public DTOHeaderBanner cabecera { get; set; }
        public DTODetalleMatricula detalle { get; set; }
    }

    public class DTODetalleMatricula
    {
        public AlumnoMatricula alumno { get; set; }
        public List<ProductoMatricula> listaProductos { get; set; }
    }

    public class AlumnoMatricula
    {
        public string codigoAlumno { get; set; }
        public string codigoUsuario { get; set; }
        public string idBanner { get; set; }
        public int pidm { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
    }

    public class ProductoMatricula
    {
        public string codigoPrograma { get; set; }
        public string descripcion { get; set; }
        public List<ListaMatricula> listaMatriculas { get; set; }
    }

    public class ListaMatricula
    {
        public string periodo { get; set; }
        public PlanEstudioMatricula planEstudio { get; set; }
        public string fechaMatricula { get; set; }
        public object codigo { get; set; }
        public object descripcion { get; set; }
        public string fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
    }

    public class PlanEstudioMatricula
    {
        public string estadoPlanEstudio { get; set; }
        public string descripcion { get; set; }
        public int correlativo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta.Banner
{
    public class DTODetalleMatriculaBannerRespuesta
    {
        public DTOHeaderBanner cabecera { get; set; }
        public DTODetalleDetalleMatricula detalle { get; set; }
    }

    public class DTODetalleDetalleMatricula
    {
        public List<AlumnoDetalleMatricula> listaAlumnos { get; set; }
    }

    public class AlumnoDetalleMatricula
    {
        public string idBanner { get; set; }
        public int pidm { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string codigoAlumno { get; set; }
        public string codigoUsuario { get; set; }
        public Lista programa { get; set; }
        public int creditosHora { get; set; }
        public List<DetalleDetalleMatricula> listaDetalleMatricula { get; set; }
    }

    public class DetalleDetalleMatricula
    {
        public string periodo { get; set; }
        public List<DetalleCursoDetalleMatricula> listaCursos { get; set; }
    }

    public class DetalleCursoDetalleMatricula
    {
        public Lista partePeriodo { get; set; }
        public string nrc { get; set; }
        public Lista materia { get; set; }
        public string numeroCurso { get; set; }
        public string titulo { get; set; }
        public string numeroSeccion { get; set; }
        public CalificacionDetalleMatricula calificacion { get; set; }
        public Lista estadoInscripcion { get; set; }
        public Lista campus { get; set; }
        public Lista metodoEducativo { get; set; }
        public int planEstudio { get; set; }
        public int horasCredito { get; set; }
        public int horasCobro { get; set; }
    }

    public class CalificacionDetalleMatricula
    {
        public string nota { get; set; }
        public Lista modoCalificacion { get; set; }
        public string fechaPaseHistoria { get; set; }
    }
}


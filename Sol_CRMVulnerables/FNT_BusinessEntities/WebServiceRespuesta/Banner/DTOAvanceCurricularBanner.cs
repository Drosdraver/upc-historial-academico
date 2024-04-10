using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta.Banner
{
    public class DTOAvanceCurricularBannerRespuesta
    {
        public DTOHeaderBanner cabecera { get; set; }
        public DTODetalleAvanceCurricular detalle { get; set; }

        public class DTODetalleAvanceCurricular
        {
            public AvanceCurricular avanceCurricular { get; set; }
        }

        public class AvanceCurricular
        {
            public string idBanner { get; set; }
            public int pidm { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string codigoAlumno { get; set; }
            public string codigoUsuario { get; set; }
            public int numeroSolicitud { get; set; }
            public Programa programa { get; set; }
            public int? totalCreditosCumplidos { get; set; }
            public int? totalAsginaturasCumplidas { get; set; }
            public List<Area> listaAreasObligatorias { get; set; }
            public List<Area> listaAreasComplementarias { get; set; }
            public List<object> listaAreasMenciones { get; set; }
        }

        public class Programa
        {
            public string codigo { get; set; }
            public string descripcionEspecial { get; set; }
        }

        public class Area
        {
            public string nombre { get; set; }
            public string descripcion { get; set; }
            public int prioridad { get; set; }
            public object cantidadCursosRequeridos { get; set; }
            public int cantidadCursosUsados { get; set; }
            public object cantidadCreditosRequeridos { get; set; }
            public int cantidadCreditosUsados { get; set; }
            public string cumplido { get; set; }
            public List<Regla> listaReglas { get; set; }
        }

        public class Regla
        {
            public string nombre { get; set; }
            public string descripcion { get; set; }
            public string cumplido { get; set; }
            public int cantidadCondicionesRequeridas { get; set; }
            public List<Curso> listaCursos { get; set; }
            public List<Examen> listaExamenes { get; set; }
        }

        public class Curso
        {
            public string titulo { get; set; }
            public string nrc { get; set; }
            public string codigoPeriodo { get; set; }
            public int? totalCreditos { get; set; }
            public Materia materia { get; set; }
            public string numeroCurso { get; set; }
            public Campus campus { get; set; }
            public Departamento departamento { get; set; }
            public Facultad facultad { get; set; }
            public Calificacion calificacion { get; set; }
        }

        public class Materia
        {
            public string codigo { get; set; }
            public string descripcion { get; set; }
        }

        public class Campus
        {
            public string codigo { get; set; }
            public string descripcion { get; set; }
        }

        public class Departamento
        {
            public string codigo { get; set; }
            public string descripcion { get; set; }
        }

        public class Facultad
        {
            public string codigo { get; set; }
            public string descripcion { get; set; }
        }

        public class Calificacion
        {
            public string notaFinal { get; set; }
            public ModoCalificacion modoCalificacion { get; set; }
            public string fechaCalificacion { get; set; }
        }

        public class ModoCalificacion
        {
            public string codigo { get; set; }
            public string descripcion { get; set; }
        }

        public class Examen
        {
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public string puntaje { get; set; }
        }
    }
}

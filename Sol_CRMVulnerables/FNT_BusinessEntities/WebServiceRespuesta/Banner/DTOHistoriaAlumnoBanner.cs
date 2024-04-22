using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNT_BusinessEntities.WebServiceRespuesta.Banner
{
    public class DTOHistoriaAlumnoBannerRespuesta
    {
        public DTOHeaderBanner cabecera { get; set; }
        public DTODetalleHistoriaAlumno detalle { get; set; }
    }

    public class DTODetalleHistoriaAlumno
    {
        public List<ListaHistoriaAlumno> listaHistoriaAlumno { get; set; }
    }

    public class ListaHistoriaAlumno
    {
        public string idBanner { get; set; }
        public int pidm { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string codigoAlumno { get; set; }
        public string codigoUsuario { get; set; }
        public double promedioGlobalAcumulado { get; set; }
        public int planEstudio { get; set; }
        public ProgramaHistoriaAlumno programa { get; set; }
        public List<CursoHistoriaAlumno> listaCursos { get; set; }
    }

    public class CursoHistoriaAlumno
    {
        public string nrc { get; set; }
        public string titulo { get; set; }
        public string nombreLargo { get; set; }
        public string codigoPeriodo { get; set; }
        public int totalCreditos { get; set; }
        public Lista materia { get; set; }
        public string numeroCurso { get; set; }
        public string numeroSeccion { get; set; }
        public Lista campus { get; set; }
        public Lista tipoHorario { get; set; }
        public PartePeriodoHistoriaAlumno partePeriodo { get; set; }
        public Lista facultad { get; set; }
        public Lista departamento { get; set; }
        public object turno { get; set; }
        public CalificacionHistoriaAlumno calificacion { get; set; }
    }

    public class CalificacionHistoriaAlumno
    {
        public string notaFinal { get; set; }
        public Lista modoCalificacion { get; set; }
        public string fechaCalificacion { get; set; }
    }

    public class PartePeriodoHistoriaAlumno
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
    }

    public class ProgramaHistoriaAlumno
    {
        public string codigo { get; set; }
        public string descripcionEspecial { get; set; }
    }

    public class Detail
    {
        public Dictionary<string, List<CursoHistoriaAlumno>> AgrupamientoCursosPeriodo(DTOHistoriaAlumnoBannerRespuesta parametro)
        {
            Dictionary<string, List<CursoHistoriaAlumno>> coursesByPeriod = new Dictionary<string, List<CursoHistoriaAlumno>>();

            foreach (var historyStudent in parametro.detalle.listaHistoriaAlumno)
            {
                foreach (var course in historyStudent.listaCursos)
                {
                    if (!coursesByPeriod.ContainsKey(course.codigoPeriodo))
                    {
                        coursesByPeriod[course.codigoPeriodo] = new List<CursoHistoriaAlumno>();
                    }

                    coursesByPeriod[course.codigoPeriodo].Add(course);
                }
            }

            return coursesByPeriod;
        }
    }
}

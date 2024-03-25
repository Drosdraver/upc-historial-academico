using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Inasistencias.
    /// </summary>
    public class DTOInasistenciasResultado
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOInasistencias> ListaDTOInasistenciasAlumnos { get; set; }
        public object ListaDTOInasistenciaSesionAlumno { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOInasistenciasResultado.
    /// </summary>
    public class DTOInasistencias
    {
        public DTOInasistenciasCab DTOInasistenciasCab { get; set; }
        public List<DTOInasistenciasDet> ListaDTOInasistenciasDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOInasistenciasResultado.
    /// </summary>
    public class DTOInasistenciasCab
    {
        public String CodLineaNegocio { get; set; }
        public String CodModalEst { get; set; }
        public String CodPeriodo { get; set; }
        public Int32? MatriculaId { get; set; }
        public String CodAlumno { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String Nombres { get; set; }
        public String CodProducto { get; set; }
        public String DesProducto { get; set; }
        public String Seccion { get; set; }
        public String Grupo { get; set; }
        public String CodCurso { get; set; }
        public String DesCurso { get; set; }
        public Int32? ClasesDictadas { get; set; }
        public Int32? ClasesAsistidas { get; set; }
        public Int32? ClasesTardanzas { get; set; }
        public Int32? ClasesInasistidas { get; set; }
        public Int32? ClasesEfectivasIna { get; set; }
        public Int32? ClasesProgramadas { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOInasistenciasResultado.
    /// </summary>
    public class DTOInasistenciasDet
    {
        public Int32? IdSesion { get; set; }
        public DateTime? FechaSesion { get; set; }
        public String EstadoInasiste { get; set; }
    }
}

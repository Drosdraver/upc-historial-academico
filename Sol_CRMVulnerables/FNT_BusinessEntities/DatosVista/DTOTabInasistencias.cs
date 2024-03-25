using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Inasistencias".
    /// </summary>
    public class DTOTabInasistencias
    {
        public List<DTOInasistenciasAsignaturas> listaInasistencias { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabInasistencias.
    /// </summary>
    public class DTOInasistenciasAsignaturas
    {
        public String CodigoCurso { get; set; }
        public String DescripcionCurso { get; set; }
        public String ClasesDictadas { get; set; }
        public String ClasesAsistidas { get; set; }
        public String ClasesInasistidas { get; set; }
        public String InasistenciasEfectivas { get; set; }
        public List<String> listaFechasInasistencia { get; set; }
        public String NombreDocente { get; set; }
    }
}

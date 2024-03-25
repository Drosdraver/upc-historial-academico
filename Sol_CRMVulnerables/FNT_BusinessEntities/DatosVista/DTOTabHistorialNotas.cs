using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Historial de notas".
    /// </summary>
    public class DTOTabHistorialNotas
    {
        public List<DTOHistorialNotas> listaHistorialNotas { get; set; } = new List<DTOHistorialNotas>();
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabHistorialNotas.
    /// </summary>
    public class DTOHistorialNotas
    {
        public String Periodo { get; set; }
        public String Carrera { get; set; }
        public List<DTOHistorialNotasDet> listaHistorialNotasDet { get; set; } = new List<DTOHistorialNotasDet>();
        public String TipoMatricula { get; set; }
        public String EstadoMatricula { get; set; }
        public String Ciclo { get; set; }
        public String PonderadoActual { get; set; }
        public String PonderadoAcumulado { get; set; }
        public String ObservacionesConst { get; set; }
        public String ObservacionesAlt { get; set; }
        public String OrdenMerito { get; set; }
        public String PertenenciaMerito { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabHistorialNotas.
    /// </summary>
    public class DTOHistorialNotasDet
    {
        public String CodigoCurso { get; set; }
        public String DescripcionCurso { get; set; }
        public String Nivel { get; set; }
        public String Creditos { get; set; }
        public String PromedioFinal { get; set; }
        public String NumeroVeces { get; set; }
        public String Aprobado { get; set; }
    }
}

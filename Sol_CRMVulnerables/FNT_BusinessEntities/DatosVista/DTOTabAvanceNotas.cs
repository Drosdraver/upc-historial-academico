using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Notas actuales".
    /// </summary>
    public class DTOTabAvanceNotas
    {
        public Double NotaRoja { get; set; }
        public List<DTOAvanceNotasCursos> listaAvanceNotas { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabAvanceNotas.
    /// </summary>
    public class DTOAvanceNotasCursos
    {
        public String CodigoCurso { get; set; }
        public String DescripcionCurso { get; set; }
        public String Seccion { get; set; }
        public String Grupo { get; set; }
        public String Vez { get; set; }
        public String Creditos { get; set; }
        public List<DTOAvanceNotasDetalle> listaDetalleNotas { get; set; }
        public String AvancePorcentual { get; set; }
        public String NotaNoOficial { get; set; }
        public String NotaProyectada { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabAvanceNotas.
    /// </summary>
    public class DTOAvanceNotasDetalle
    {
        public String CodigoTipoPrueba { get; set; }
        public String DescripcionTipoPrueba { get; set; }
        public String NumeroPrueba { get; set; }
        public String PesoPonderado { get; set; }
        public String Nota { get; set; }
        public String ActualizadoPor { get; set; }
        public String FechaActualizacion { get; set; }
        public String Observaciones { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Promedio ponderado".
    /// </summary>
    public class DTOTabPromedioPonderado
    {
        public Double promedioRojo { get; set; }
        public Double promedioNaranja { get; set; }
        public List<DTOPromedioPonderado> listaPromediosPonderados { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabPromedioPonderado.
    /// </summary>
    public class DTOPromedioPonderado
    {
        public String CodigoPeriodo { get; set; }
        public Double PromedioPonderado { get; set; }
        public Double PorcentajeNota { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Deudas".
    /// </summary>
    public class DTOTabDeudas
    {
        public String FechaActual { get; set; }
        public String CodigoAlumno { get; set; }
        public String NombreAlumno { get; set; }
        public List<DTODeudaFila> listaFilasDeuda { get; set; }
        public Double ImporteSoles { get; set; }
        public Double MoraSoles { get; set; }
        public Double TotalSoles { get; set; }
        public Double ImporteDolares { get; set; }
        public Double MoraDolares { get; set; }
        public Double TotalDolares { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabDeudas.
    /// </summary>
    public class DTODeudaFila
    {
        public String Documento { get; set; }
        public String FechaEmision { get; set; }
        public String FechaVencimiento { get; set; }
        public String TipoVenta { get; set; }
        public String Moneda { get; set; }
        public Double Importe { get; set; }
        public Double Mora { get; set; }
        public Double Total { get; set; }
    }
}

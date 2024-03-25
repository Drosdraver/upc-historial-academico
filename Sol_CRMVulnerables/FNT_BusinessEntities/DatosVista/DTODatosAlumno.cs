using System;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos del alumno a mostrar en la zona superior derecha del aplicativo.
    /// </summary>
    public class DTODatosAlumno
    {
        public String Nombres { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String CodigoUsuario { get; set; }
        public String UrlImagen { get; set; }
    }
}

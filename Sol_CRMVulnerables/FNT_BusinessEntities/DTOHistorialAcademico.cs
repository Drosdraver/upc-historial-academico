using FNT_BusinessEntities.DatosVista;

namespace FNT_BusinessEntities
{
    /// <summary>
    /// Clase que contiene la respuesta final de la lectura y procesamiento de todos los datos del aplicativo.
    /// </summary>
    public class DTOHistorialAcademico
    {
        public DTOParametrosServicios ParametrosServicios { get; set; }
        public DTODatosVista DatosVista { get; set; }
        public bool RespuestaExitosa { get; set; }
        public string MensajeError { get; set; }
    }

    /// <summary>
    /// Clase con los parámetros URL empleados para llamar a los servicios web.
    /// </summary>
    public class DTOParametrosServicios
    {
        public string CodLineaNegocio { get; set; }
        public string CodAlumno { get; set; }
        public string CodModalEst { get; set; }
        public string CodPeriodo { get; set; }
        public string CodProducto { get; set; }
        public string CodCurriculo { get; set; }
        public string FechaSesion1 { get; set; }
        public string FechaSesion2 { get; set; }
        public string CodUsuario { get; set; }
        public string ClienteCobrara { get; set; }
    }

    public class DTOParametrosServiciosBanner
    {
        public string CodNivelBanner { get; set; }
        public string CodAlumnoBanner { get; set; }
        public string CodUsuarioBanner { get; set; }
        public string CodPeriodoBanner { get; set; }
        public string CodProgramaBanner { get; set; }
        public int CodPidmBanner { get; set; }
        public string DescripcionNivelBanner { get; set; }
        public string DescripcionProgramalBanner { get; set; }
        public string IdBanner { get; set; }
        public string CodModalidadBanner { get; set; }
    }

    /// <summary>
    /// Clase con los datos procesados de cada una de las secciones del aplicativo.
    /// </summary>
    public class DTODatosVista
    {
        public DTODatosAlumno DTODatosAlumno { get; set; }
        public DTOTabDatosGenerales DTOTabDatosGenerales { get; set; }
        public DTOTabAvanceCurricular DTOTabAvanceCurricular { get; set; }
        public DTOTabHistorialNotas DTOTabHistorialNotas { get; set; }
        public DTOTabHorarioAlumno DTOTabHorarioAlumno { get; set; }
        public DTOTabAvanceNotas DTOTabAvanceNotas { get; set; }
        public DTOTabInasistencias DTOTabInasistencias { get; set; }
        public DTOTabDeudas DTOTabDeudas { get; set; }
        public DTOTabPromedioPonderado DTOTabPromedioPonderado { get; set; }
        public DTOTabTramites DTOTabTramites { get; set; }
    }
}

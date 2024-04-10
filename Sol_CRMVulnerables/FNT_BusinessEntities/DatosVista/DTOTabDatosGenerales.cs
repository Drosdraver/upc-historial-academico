using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.DatosVista
{
    /// <summary>
    /// Clase con los datos a emplear para llenar la pestaña de "Datos generales".
    /// </summary>
    public class DTOTabDatosGenerales
    {
        public List<DTODatosGeneralesPeriodos> listaPeriodos { get; set; } = new List<DTODatosGeneralesPeriodos>();
        public List<DTODatosGeneralesModalidades> listaModalidades { get; set; } = new List<DTODatosGeneralesModalidades>();
        public String CodPeriodo { get; set; }
        public String CodModalidad { get; set; }
        public String Facultad { get; set; }
        public String Carrera { get; set; }
        public List<DTODatosGeneralesPorPeriodo> listaDatosPorPeriodo { get; set; }
        public List<DTODatosGeneralesHechosImportantes> listaHechosImportantes { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabDatosGenerales.
    /// </summary>
    public class DTODatosGeneralesPeriodos
    {
        public String PeriodoVal { get; set; }
        public String PeriodoDes { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabDatosGenerales.
    /// </summary>
    public class DTODatosGeneralesModalidades
    {
        public String ModalidadVal { get; set; }
        public String ModalidadDes { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabDatosGenerales.
    /// </summary>
    public class DTODatosGeneralesPorPeriodo
    {
        public String CodPeriodo { get; set; }
        public String Campus { get; set; }
        public String CicloAlumno { get; set; }
        public String EstadoMatricula { get; set; }
        public String Categoria { get; set; }
        public String Orden { get; set; }
        public String OrdenMeritoAcumulado { get; set; }
        public String TipoMeritoCarrera { get; set; }
        public String TipoMeritoCarreraAcumulado { get; set; }
        public String PonderadoActual { get; set; }
        public String PonderadoAcumulado { get; set; }
        public String PonderadoBeca { get; set; }
        public String Egresado { get; set; }
        public String Pronabec { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOTabDatosGenerales.
    /// </summary>
    public class DTODatosGeneralesHechosImportantes
    {
        public String FechaHecho { get; set; }
        public String HoraHecho { get; set; }
        public String TipoRegistro { get; set; }
        public String Descripcion { get; set; }
        public String RegistradoPor { get; set; }
        public String Activo { get; set; }
        public String PeriodoEliminado { get; set; }
    }
}

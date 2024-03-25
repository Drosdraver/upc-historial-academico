using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Alumnos Facturables.
    /// </summary>
    public class DTOAlumnosFacturablesRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOAlumnosFacturables> ListaDTOAlumnosFacturables { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAlumnosFacturablesRespuesta.
    /// </summary>
    public class DTOAlumnosFacturables
    {
        public DTOAlumnosFacturablesCab DTOAlumnosFacturablesCab { get; set; }
        public List<ListaDTOAlumnosFacturablesDet> ListaDTOAlumnosFacturablesDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAlumnosFacturablesRespuesta.
    /// </summary>
    public class DTOAlumnosFacturablesCab
    {
        public Int32? IdFacturable { get; set; }
        public String CodLineaNegocio { get; set; }
        public String CodModalEst { get; set; }
        public String NombreModal { get; set; }
        public String CodPeriodo { get; set; }
        public Int32? IdGrupo { get; set; }
        public String DesGrupo { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAlumnosFacturablesRespuesta.
    /// </summary>
    public class ListaDTOAlumnosFacturablesDet
    {
        public String CodAlumno { get; set; }
        public String ApePatImag { get; set; }
        public String ApeMatImag { get; set; }
        public String NombresImag { get; set; }
        public String ApellidoPatern { get; set; }
        public String ApellidoMatern { get; set; }
        public String Nombres { get; set; }
        public String TipoDocumento { get; set; }
        public String DocumenIdentida { get; set; }
        public String CodEmpresa { get; set; }
        public String RazonSocial { get; set; }
        public String CodCategoria { get; set; }
        public String DesCategoria { get; set; }
        public Int32? CodModalidadPago { get; set; }
        public String DesModalidadPago { get; set; }
        public Int32? NumCuotasModalidad { get; set; }
        public Int32? CodModalFactur { get; set; }
        public String DesModalFactur { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public String Estado { get; set; }
        public DateTime? FechaInactivacion { get; set; }
        public String Glosa { get; set; }
        public String Sede { get; set; }
        public String DesSede { get; set; }
        public String CodMotivo { get; set; }
        public String DesMotivo { get; set; }
        public Double? HorasApoyo { get; set; }
        public String Exonerado { get; set; }
        public String Modulo { get; set; }
        public String TipoEje { get; set; }
    }
}

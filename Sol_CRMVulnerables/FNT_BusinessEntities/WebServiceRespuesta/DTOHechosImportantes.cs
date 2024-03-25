using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Hechos Importantes.
    /// </summary>
    public class DTOHechosImportantesRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOHechosImportantes> ListaHechoImportantes { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOHechosImportantesRespuesta.
    /// </summary>
    public class DTOHechosImportantes
    {
        public DTOHechoImportantesCab DTOHechoImportantesCab { get; set; }
        public List<DTOHechoImportantesDet> DTOHechoImportantesDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOHechosImportantesRespuesta.
    /// </summary>
    public class DTOHechoImportantesCab
    {
        public String CodLineaNegocio { get; set; }
        public String CodModalEstudio { get; set; }
        public String DesModalEstudio { get; set; }
        public String CodAlumno { get; set; }
        public Int32? CodPersona { get; set; }
        public String ApePaternoImag { get; set; }
        public String ApeMaternoImag { get; set; }
        public String NombresImag { get; set; }
        public String ApePaterno { get; set; }
        public String ApeMaterno { get; set; }
        public String Nombres { get; set; }
        public String TipoDocumento { get; set; }
        public String DesTipoDocumento { get; set; }
        public String DocIdentidad { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOHechosImportantesRespuesta.
    /// </summary>
    public class DTOHechoImportantesDet
    {
        public Int32? IdHecho { get; set; }
        public Int32? CodTipoRegistro { get; set; }
        public String DesTipoRegistro { get; set; }
        public String TipoHecho { get; set; }
        public String Hecho { get; set; }
        public String CodProducto { get; set; }
        public String DesProducto { get; set; }
        public Int32? NumSancion { get; set; }
        public String CodPeriodCambio { get; set; }
        public String NumReanud { get; set; }
        public Int32? MatriculaId { get; set; }
        public String Estado { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public DateTime? FecModificacion { get; set; }
    }
}

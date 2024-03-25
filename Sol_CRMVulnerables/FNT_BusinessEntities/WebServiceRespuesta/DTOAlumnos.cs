using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Alumnos.
    /// </summary>
    public class DTOAlumnosRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOAlumnos> ListaDTOAlumnos { get; set; }
        public DTOAlumnos DTOAlumnos { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOAlumnosRespuesta.
    /// </summary>
    public class DTOAlumnos
    {
        public String CodLineaNegocio { get; set; }
        public String CodAlumno { get; set; }
        public Double CodPersona { get; set; }
        public String CodUsuario { get; set; }
        public String EstadoIngreso { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public String IndEnvioCarta { get; set; }
        public String IndDireccionErrada { get; set; }
        public String CodDestinatario { get; set; }
        public String PIN { get; set; }
        public String FlagCorreo { get; set; }
        public String CodSede { get; set; }
        public String ApePatImag { get; set; }
        public String ApeMatImag { get; set; }
        public String NombresImag { get; set; }
        public String ApellidoPatern { get; set; }
        public String ApellidoMatern { get; set; }
        public String Nombres { get; set; }
        public String TipoDocumento { get; set; }
        public String DocumenIdentida { get; set; }
        public String LineaNegocioNombre { get; set; }
        public String CodModalidadEstActual { get; set; }
        public String CodPeriodoActual { get; set; }
        public String CodProductoActual { get; set; }
        public String UsuarioEmail { get; set; }
        public String FechaNacimiento { get; set; }
        public String FotoUrl { get; set; }
        public Double UltIdMatricula { get; set; }
        public String CodSedeMatricula { get; set; }
        public String Ciclo { get; set; }
        public String DesProducto { get; set; }
    }
}
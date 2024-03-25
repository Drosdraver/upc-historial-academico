using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Carrera Profesional.
    /// </summary>
    public class DTOCarreraProfesionalRespuesta
    {
        public DTOHeader oDTOHeader { get; set; }
        public List<DTOCarreraProfesional> oDTOListaCarreraProfesional { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOCarreraProfesionalRespuesta.
    /// </summary>
    public class DTOCarreraProfesional
    {
        public DTOCarreraProfesionalCab oDTOCarreraProfesionalCab { get; set; }
        public List<DTOListaCarreraProfesionalDet> oDTOListaCarreraProfesionalDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOCarreraProfesionalRespuesta.
    /// </summary>
    public class DTOCarreraProfesionalCab
    {
        public String CodLineaNegocio { get; set; }
        public String LineaNegocioNombre { get; set; }
        public String CodModalEst { get; set; }
        public String ModalidadEstudNombre { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOCarreraProfesionalRespuesta.
    /// </summary>
    public class DTOListaCarreraProfesionalDet
    {
        public String CodProducto { get; set; }
        public String Descripcion { get; set; }
        public String DescripcionCorta { get; set; }
        public String DescEspecial { get; set; }
        public String CodFacultad { get; set; }
        public String FacultadDescripcion { get; set; }
        public String CodTipoProduc { get; set; }
        public String TipoProducDescripcion { get; set; }
        public String EsCarrera { get; set; }
        public String CodLineaArea { get; set; }
        public String CodArea { get; set; }
        public String CodTarifa { get; set; }
        public String EstadoReqEspecial { get; set; }
        public String CodTipoActividad { get; set; }
        public String EstadoMatricula { get; set; }
        public String CodLinea { get; set; }
        public String CodFamilia { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public Double? PorcentajeComision { get; set; }
        public String ReqSecigra { get; set; }
        public String CodDirector { get; set; }
        public String CodEncargado { get; set; }
        public Int32? NumAnexo { get; set; }
        public String CreaUsuario { get; set; }
        public String CodStatus { get; set; }
        public String CodUnidad { get; set; }
        public String CodFacultadProd { get; set; }
        public String CodEspecialidad { get; set; }
        public String TransfEquiv { get; set; }
        public Int32? IdGrupo { get; set; }
        public String CodigoAleph { get; set; }
        public String Caracter { get; set; }
        public Int32? CantCiclos { get; set; }
        public String IndAgregacion { get; set; }
        public String Especial { get; set; }
        public String FueraInstit { get; set; }
        public String Anexo { get; set; }
        public String IndLibre { get; set; }
        public String Anual { get; set; }
        public String Internado { get; set; }
        public String IndPronabec { get; set; }
        public String IndAdmision { get; set; }
        public String TarifaDescripcion { get; set; }
        public String FamiliaDescripcion { get; set; }
        public String PersonaNombreDirector { get; set; }
        public String PersonaNombreEncargado { get; set; }
        public String IndCurriculo { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace FNT_BusinessEntities.WebServiceRespuesta
{
    /// <summary>
    /// Clase que contiene la respuesta del servicio de Matriculas.
    /// </summary>
    public class DTOMatriculasRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOMatriculas> ListaDTOMatricula { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMatriculasRespuesta.
    /// </summary>
    public class DTOMatriculas
    {
        public DTOMatriculaCab DTOMatriculaCab { get; set; }
        public List<DTOMatriculaDet> ListaDTOMatriculaDet { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMatriculasRespuesta.
    /// </summary>
    public class DTOMatriculaCab
    {
        public String CodLineaNegocio { get; set; }
        public String CodModalEst { get; set; }
        public String CodAlumno { get; set; }
        public String CodUsuarioAlumno { get; set; }
        public String ApePatImag { get; set; }
        public String ApeMatImag { get; set; }
        public String NombresImag { get; set; }
        public String ApellidoPatern { get; set; }
        public String ApellidoMatern { get; set; }
        public String Nombres { get; set; }
        public String TipoDocumento { get; set; }
        public String DocumenIdentida { get; set; }
    }

    /// <summary>
    /// Clase forma parte de la estructura de la clase DTOMatriculasRespuesta.
    /// </summary>
    public class DTOMatriculaDet
    {
        public Int32? MatriculaId { get; set; }
        public String CodPeriodMat { get; set; }
        public String CodProducMat { get; set; }
        public Int32? CodCurricMAt { get; set; }
        public String CodSeccion { get; set; }
        public String CodEstadoMat { get; set; }
        public String CodTipoMat { get; set; }
        public String CicloAlumno { get; set; }
        public String CodExamen { get; set; }
        public String EstadoEgresado { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public String UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
        public Double? PondActual { get; set; }
        public Double? PondAcumulado { get; set; }
        public Double? PondBeca { get; set; }
        public Double? NumObsCons { get; set; }
        public Double? NumObsAlte { get; set; }
        public Double? OrdenMerito { get; set; }
        public Double? OrdenBecaMerito { get; set; }
        public String EstadoCierre { get; set; }
        public String EstadoMatExtem { get; set; }
        public Double? OrdenMeritoProd { get; set; }
        public Double? OrdenBecaMeritoProd { get; set; }
        public String TipoMerito { get; set; }
        public String TipoMeritoProd { get; set; }
        public Double? OrdenMeritoAcum { get; set; }
        public Double? OrdenMeritoProdAcum { get; set; }
        public String TipoMeritoAcum { get; set; }
        public String TipoMeritoProdAcum { get; set; }
        public String Glosa { get; set; }
        public String CodEmpresa { get; set; }
        public String CodSede { get; set; }
        public String CodTipoTarifa { get; set; }
        public String MatricEsDocenteAli { get; set; }
        public String CodPersonaContacto { get; set; }
        public Int32? IdReserva { get; set; }
        public String Prueba { get; set; }
        public String CopyCicAlu { get; set; }
        public String CodLocal { get; set; }
        public String MatAdicional { get; set; }
        public String IndPronabec { get; set; }
        public String DescSede { get; set; }
        public String DescProducto { get; set; }
        public String DescEstadoMatric { get; set; }
        public String DescTipoMatric { get; set; }
        public String DescTipoTarifa { get; set; }
        public String DescTipoMotivoMatricAdic { get; set; }
    }
}
